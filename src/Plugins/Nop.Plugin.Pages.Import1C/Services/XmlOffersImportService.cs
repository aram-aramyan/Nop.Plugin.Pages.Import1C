using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Shipping;
using Nop.Services.Catalog;
using Nop.Services.Shipping;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal static class XmlOffersImportService
    {
        //private const string PRODUCT_TABLE_NAME = "Product";

        internal static void Import(КоммерческаяИнформация source,
            IShippingService shippingService,
            IProductService productService,
            string warehouseMappingsFile,
            string mappingsFile,
            string logFile)
        {

            var warehouseMappings = ImportWarehouses(source, shippingService, warehouseMappingsFile, logFile);

            ImportOffers(source, productService, warehouseMappings, mappingsFile, logFile);
        }

        private static void ImportOffers(КоммерческаяИнформация source,
            IProductService productService,
            Dictionary<string, int> warehouseMappings,
            string mappingsFile,
            string logFile)
        {
            var stats = new[] { 0 };
            logFile.Log("Начало импорта предложений");

            var mappings = File.Exists(mappingsFile)
                ? JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(mappingsFile))
                : new Dictionary<string, int>();

            var sitePrice = new[] { source.ПакетПредложений.ТипыЦен.ТипЦены }.ToList()
                .FirstOrDefault(t => t.Наименование == "Для сайта");

            var productsToSave = new List<Product>();

            foreach (var offer in source.ПакетПредложений.Предложения)
            {
                // только те продукты, которые были ранее добавлены
                if (mappings.ContainsKey(offer.Ид))
                {
                    var isDirty = false;
                    var product = productService.GetProductById(mappings[offer.Ид]);

                    //todo: скорее всего их будет потом несколько
                    var whs = new[] { offer.Склад }.ToList();

                    //todo: пока поддерживаем только 1 склад, потом нужно будет написать отдельный код для нескольких складов
                    if (whs.Count > 0)
                    {
                        var wh = whs[0];
                        var whId = warehouseMappings.ContainsKey(wh.ИдСклада) ? warehouseMappings[wh.ИдСклада] : 0;
                        if (product.WarehouseId != whId)
                        {
                            isDirty = true;
                            product.WarehouseId = whId;
                        }

                        var quantity = wh.КоличествоНаСкладе;
                        if (product.StockQuantity != quantity)
                        {
                            isDirty = true;
                            product.StockQuantity = quantity;
                        }

                        //if (quantity > 0 && product.ManageInventoryMethod == ManageInventoryMethod.DontManageStock)
                        //{
                        //    isDirty = true;
                        //    product.ManageInventoryMethod = ManageInventoryMethod.ManageStock;
                        //}
                    }
                    else if (product.WarehouseId > 0)
                    {
                        isDirty = true;
                        product.WarehouseId = 0;
                    }


                    if (sitePrice != null)
                    {
                        var price = new[] { offer.Цены.Цена }.ToList().FirstOrDefault(p => p.ИдТипаЦены == sitePrice.Ид);
                        if (price != null)
                        {
                            if (product.Price != price.ЦенаЗаЕдиницу)
                            {
                                isDirty = true;
                                product.Price = price.ЦенаЗаЕдиницу;
                            }
                        }
                    }


                    if (isDirty)
                    {
                        productsToSave.Add(product);
                        stats[0]++;
                    }
                }
            }

            if (productsToSave.Count > 0)
            {
                productService.UpdateProducts(productsToSave);
            }

            logFile.Log($"Импорт предложений завершен. Обновлено: {stats[0]}.");
        }

        private static Dictionary<string, int> ImportWarehouses(КоммерческаяИнформация source,
            IShippingService shippingService,
            string warehouseMappingsFile,
            string logFile)
        {
            var stats = new[] { 0 };
            logFile.Log("Начало импорта складов");
            var warehouseMappings = File.Exists(warehouseMappingsFile)
                ? JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(warehouseMappingsFile))
                : new Dictionary<string, int>();

            //todo: скорее всего класс изменится, когда будет больше складов, поэтому делаю коллекцию
            var whs = new[] { source.ПакетПредложений.Склады.Склад }.ToList();
            var warehouses = shippingService.GetAllWarehouses();
            foreach (var wh in whs)
            {
                if (!warehouseMappings.ContainsKey(wh.Ид)
                    || warehouses.All(w => w.Id != warehouseMappings[wh.Ид]))
                {
                    var warehouse = new Warehouse
                    {
                        Name = wh.Наименование,
                        AdminComment = wh.Ид
                    };
                    shippingService.InsertWarehouse(warehouse);
                    warehouseMappings[wh.Ид] = warehouse.Id;
                    stats[0]++;
                }
            }

            File.WriteAllText(warehouseMappingsFile, JsonConvert.SerializeObject(warehouseMappings, Formatting.Indented),
                Encoding.UTF8);

            logFile.Log($"Импорт складов завершен. Добавлено: {stats[0]}.");
            return warehouseMappings;
        }
    }
}