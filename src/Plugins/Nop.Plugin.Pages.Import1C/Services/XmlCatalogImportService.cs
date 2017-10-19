using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal class XmlCatalogImportService
    {
        internal static void Import(КоммерческаяИнформация source,
            ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            IManufacturerService manufacturerService,
            IProductService productService,
            List<Category> categories,
            Dictionary<string, int> categoryMappings,
            List<SpecificationAttribute> attributes,
            Dictionary<string, int> attributesMappings,
            List<Manufacturer> manufacturers,
            Dictionary<string, int> manufacturersMappings,
            string mappingsFile,
            string logFile)

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

                    if (product == null)
                        product = productService.GetProductBySku(prod.Артикул);

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
                        logFile.Log($"Новый товар {product.Name} ({product.Id}): {prod.Ид}");
                        stats[0]++;
                    }
                    else
                    {
                        product.UpdatedOnUtc = DateTime.Now;
                        product.Deleted = deleted;
                        product.Published = !deleted;
                        productService.UpdateProduct(product);
                        logFile.Log($"Обновлен товар {product.Name} ({product.Id}): {prod.Ид}");
                        stats[1]++;
                    }
                    mappings[prod.Ид] = product.Id;


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

                    if (prod.ЗначенияСвойств != null)
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
                    File.WriteAllText(mappingsFile, JsonConvert.SerializeObject(mappings, Formatting.Indented), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    logFile.Log($"Ошибка при обработке товара {prod.Ид} ({prod.Наименование}): {ex}");
                }
            }

            logFile.Log($"Импорт товаров завершен. Добавлено: {stats[0]}. Обновлено: {stats[1]}.");
        }
    }
}
