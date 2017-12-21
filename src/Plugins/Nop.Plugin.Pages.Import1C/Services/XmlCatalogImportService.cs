using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Services.Media;
using Nop.Services.Seo;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal class XmlCatalogImportService
    {
        public class ImportCatalogSettings
        {
            public bool UpdateExisting;
            public bool ImportSpecificationAttributes;
            public bool OverwriteCategories;
            public bool OverwriteManufacturers;
        }

        internal static void Import(КоммерческаяИнформация source,
            ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IUrlRecordService urlRecordService,
            IPictureService pictureService,
            List<Category> categories,
            Dictionary<string, int> categoryMappings,
            List<SpecificationAttribute> attributes,
            Dictionary<string, int> attributesMappings,
            List<Manufacturer> manufacturers,
            Dictionary<string, int> manufacturersMappings,
            string mappingsFile,
            string logFile,
            ImportCatalogSettings importSettings)

        {
            logFile.Log("Начало импорта товаров");
            var stats = new[] { 0, 0 };

            var mappings = File.Exists(mappingsFile)
                ? JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(mappingsFile))
                : new Dictionary<string, int>();

            foreach (var prod in source.Каталог.Товары)
            {
                try
                {
                    var product = mappings.ContainsKey(prod.Ид)
                        ? productService.GetProductById(mappings[prod.Ид])
                        : null;

                    var deleted = prod.Статус == "Удален";
                    if (product == null)
                    {
                        product = new Product
                        {
                            CreatedOnUtc = DateTime.Now,
                            UpdatedOnUtc = DateTime.Now,
                            ProductType = ProductType.SimpleProduct,
                            ProductTemplateId = 1,
                            VisibleIndividually = true,
                            Name = prod.Наименование,
                            Sku = prod.Артикул,
                            ShortDescription = prod.Описание,
                            FullDescription = prod.Описание,
                            AllowCustomerReviews = true,
                            Deleted = deleted,
                            Published = !deleted
                        };
                        productService.InsertProduct(product);
                        var seName = product.ValidateSeName(null, product.Name, true);
                        urlRecordService.SaveSlug(product, seName, 0);
                        logFile.Log($"Новый товар {product.Name} ({product.Id}): {prod.Ид}");
                        stats[0]++;
                    }
                    else
                    {
                        if (!importSettings.UpdateExisting)
                        {
                            logFile.Log($"Пропущен товар {product.Name} ({product.Id}): {prod.Ид}");
                            stats[1]++;
                            continue;
                        }
                        product.UpdatedOnUtc = DateTime.Now;
                        product.Deleted = deleted;
                        product.Published = !deleted;
                        productService.UpdateProduct(product);
                        logFile.Log($"Обновлен товар {product.Name} ({product.Id}): {prod.Ид}");
                        stats[1]++;
                    }
                    mappings[prod.Ид] = product.Id;

                    if (importSettings.OverwriteManufacturers)
                    {
                        foreach (var manufacturer in product.ProductManufacturers.ToList())
                        {
                            manufacturerService.DeleteProductManufacturer(manufacturer);
                        }
                    }
                    if (prod.Изготовитель != null && manufacturersMappings.ContainsKey(prod.Изготовитель.Ид))
                    {
                        var manufacturerId = manufacturersMappings[prod.Изготовитель.Ид];
                        var manufacturer = product.ProductManufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
                        if (manufacturer == null)
                        {
                            manufacturer = new ProductManufacturer
                            {
                                ProductId = product.Id,
                                ManufacturerId = manufacturerId
                            };
                            manufacturerService.InsertProductManufacturer(manufacturer);
                            product.ProductManufacturers.Add(manufacturer);
                        }
                    }

                    if (importSettings.OverwriteCategories)
                    {
                        foreach (var category in product.ProductCategories.ToList())
                        {
                            categoryService.DeleteProductCategory(category);
                        }
                    }
                    if (prod.Группы != null && categoryMappings.ContainsKey(prod.Группы.Ид))
                    {
                        var categoryId = categoryMappings[prod.Группы.Ид];
                        var category = product.ProductCategories.FirstOrDefault(c => c.CategoryId == categoryId);
                        if (category == null)
                        {
                            category = new ProductCategory
                            {
                                ProductId = product.Id,
                                CategoryId = categoryId
                            };
                            categoryService.InsertProductCategory(category);
                            product.ProductCategories.Add(category);
                        }
                    }

                    if (importSettings.ImportSpecificationAttributes && prod.ЗначенияСвойств != null)
                    {
                        foreach (var attr in prod.ЗначенияСвойств)
                        {
                            if (attributesMappings.ContainsKey(attr.Ид))
                            {
                                var emptyOptionKey = $"{attr.Ид}.";
                                if (attributesMappings.ContainsKey(emptyOptionKey))
                                {
                                    var option = product.ProductSpecificationAttributes
                                        .FirstOrDefault(a => a.SpecificationAttributeOptionId == attributesMappings[emptyOptionKey]);
                                    if (option == null)
                                    {
                                        option = new ProductSpecificationAttribute
                                        {
                                            ProductId = product.Id,
                                            ShowOnProductPage = true,
                                            AllowFiltering = false,
                                            SpecificationAttributeOptionId = attributesMappings[emptyOptionKey],
                                            AttributeType = SpecificationAttributeType.CustomText,
                                            CustomValue = attr.Значение
                                        };
                                        specificationAttributeService.InsertProductSpecificationAttribute(option);
                                        product.ProductSpecificationAttributes.Add(option);
                                    }
                                    else if (option.CustomValue != attr.Значение)
                                    {
                                        option.CustomValue = attr.Значение;
                                        specificationAttributeService.UpdateProductSpecificationAttribute(option);
                                    }
                                }
                                else
                                {
                                    var optionKey = $"{attr.Ид}.{attr.Значение}";

                                    var option = product.ProductSpecificationAttributes
                                        .FirstOrDefault(o => o.SpecificationAttributeOptionId == attributesMappings[optionKey]);
                                    if (option == null)
                                    {
                                        var attribute = attributes.FirstOrDefault(a => a.Id == attributesMappings[attr.Ид]);
                                        var optionIds = attribute.SpecificationAttributeOptions.Select(o => o.Id);
                                        var options = product.ProductSpecificationAttributes
                                        .Where(a => optionIds.Contains(a.SpecificationAttributeOptionId)).ToList();
                                        if (options != null && options.Count > 0)
                                            foreach (var opt in options)
                                            {
                                                product.ProductSpecificationAttributes.Remove(opt);
                                                specificationAttributeService.DeleteProductSpecificationAttribute(opt);
                                            }

                                        option = new ProductSpecificationAttribute
                                        {
                                            ProductId = product.Id,
                                            ShowOnProductPage = true,
                                            AllowFiltering = true,
                                            SpecificationAttributeOptionId = attributesMappings[optionKey],
                                            AttributeType = SpecificationAttributeType.Option
                                        };
                                        specificationAttributeService.InsertProductSpecificationAttribute(option);
                                        product.ProductSpecificationAttributes.Add(option);

                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(prod.Картинка))
                    {
                        // todo: compare with existing images
                        var picturePath = HttpContext.Current.Request.MapPath($"~/Content/{prod.Картинка}");
                        var picture = LoadPicture(pictureService, picturePath, product.Name);
                        if (picture != null)
                        {
                            product.ProductPictures.Add(new ProductPicture
                            {
                                DisplayOrder = 1,
                                PictureId = picture.Id,
                                ProductId = product.Id
                            });
                        }
                    }

                    File.WriteAllText(mappingsFile, JsonConvert.SerializeObject(mappings, Formatting.Indented), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    logFile.Log($"Ошибка при обработке товара {prod.Ид} ({prod.Наименование}): {ex}");
                }
            }

            logFile.Log($"Импорт товаров завершен. Добавлено: {stats[0]}. Обновлено: {stats[1]}.");
        }

        private static Picture LoadPicture(IPictureService pictureService, string picturePath, string name,
            int? picId = null)
        {
            if (string.IsNullOrEmpty(picturePath) || !File.Exists(picturePath))
                return null;

            var mimeType = GetMimeTypeFromFilePath(picturePath);
            var newPictureBinary = File.ReadAllBytes(picturePath);
            var pictureAlreadyExists = false;
            if (picId != null)
            {
                //compare with existing product pictures
                var existingPicture = pictureService.GetPictureById(picId.Value);

                var existingBinary = pictureService.LoadPictureBinary(existingPicture);
                //picture binary after validation (like in database)
                var validatedPictureBinary = pictureService.ValidatePicture(newPictureBinary, mimeType);
                if (existingBinary.SequenceEqual(validatedPictureBinary) ||
                    existingBinary.SequenceEqual(newPictureBinary))
                    pictureAlreadyExists = true;
            }

            if (pictureAlreadyExists) return null;

            var newPicture = pictureService.InsertPicture(newPictureBinary, mimeType,
                pictureService.GetPictureSeName(name));
            return newPicture;
        }

        private static string GetMimeTypeFromFilePath(string filePath)
        {
            var mimeType = MimeMapping.GetMimeMapping(filePath);

            //little hack here because MimeMapping does not contain all mappings (e.g. PNG)
            if (mimeType == MimeTypes.ApplicationOctetStream)
                mimeType = MimeTypes.ImageJpeg;

            return mimeType;
        }
    }
}
