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

            var productIds = mappings.Values.ToArray();

            var products = productIds.Length > 0
                ? productService.GetProductsByIds(mappings.Values.ToArray())
                : new List<Product>();

            foreach (var prod in source.Каталог.Товары)
            {
                try
                {
                    var product = mappings.ContainsKey(prod.Ид)
                        ? products.FirstOrDefault(p => p.Id == mappings[prod.Ид])
                        : null;

                    if (product == null)
                    {
                        product = new Product
                        {
                            CreatedOnUtc = DateTime.Now,
                            ProductTypeId = 5,
                            ProductTemplateId = 1,
                            VisibleIndividually = true,
                            Name = prod.Наименование,
                            ShortDescription = prod.Описание,
                            FullDescription = prod.Описание,
                            AllowCustomerReviews = true,
                        };
                        productService.InsertProduct(product);
                        mappings[prod.Ид] = product.Id;
                        logFile.Log($"Новый товар {product.Name} ({product.Id}): {prod.Ид}");
                        stats[0]++;
                    }
                    else
                    {
                        logFile.Log($"Обновлен товар {product.Name} ({product.Id}): {prod.Ид}");
                        stats[1]++;
                    }

                    product.UpdatedOnUtc = DateTime.Now;
                    product.Sku = prod.Артикул;
                    product.Deleted = prod.Статус == "Удален";
                    product.Published = !product.Deleted;

                    if (prod.Изготовитель != null && manufacturersMappings.ContainsKey(prod.Изготовитель.Ид))
                    {
                        var manufacturerId = manufacturersMappings[prod.Изготовитель.Ид];
                        var manufacturer = product.ProductManufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
                        if (manufacturer == null)
                        {
                            product.ProductManufacturers.Add(
                                new ProductManufacturer
                                {
                                    ProductId = product.Id,
                                    ManufacturerId = manufacturerId
                                });
                        }
                    }

                    if (prod.Группы != null && categoryMappings.ContainsKey(prod.Группы.Ид))
                    {
                        var categoryId = categoryMappings[prod.Группы.Ид];
                        var category = product.ProductCategories.FirstOrDefault(c => c.CategoryId == categoryId);
                        if (category == null)
                        {
                            product.ProductCategories.Add(
                                new ProductCategory
                                {
                                    ProductId = product.Id,
                                    CategoryId = categoryId
                                });
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
                                        product.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute
                                        {
                                            ProductId = product.Id,
                                            SpecificationAttributeOptionId = attributesMappings[emptyOptionKey],
                                            AttributeType = SpecificationAttributeType.CustomText,
                                            CustomValue = attr.Значение
                                        });
                                    }
                                    else if (option.CustomValue != attr.Значение)
                                    {
                                        option.CustomValue = attr.Значение;
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
                                                product.ProductSpecificationAttributes.Remove(opt);

                                        product.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute
                                        {
                                            ProductId = product.Id,
                                            SpecificationAttributeOptionId = attributesMappings[optionKey],
                                            AttributeType = SpecificationAttributeType.Option
                                        });
                                    }
                                }
                            }
                        }
                    }

                    productService.UpdateProduct(product);
                }
                catch (Exception ex)
                {
                    logFile.Log($"Ошибка при обработке товара {prod.Ид} ({prod.Наименование}): {ex}");
                }
            }

            File.WriteAllText(mappingsFile, JsonConvert.SerializeObject(mappings, Formatting.Indented), Encoding.UTF8);
            logFile.Log("Импорт товаров завершен. Добавлено: . Обновлено: .");
        }


    }
}
