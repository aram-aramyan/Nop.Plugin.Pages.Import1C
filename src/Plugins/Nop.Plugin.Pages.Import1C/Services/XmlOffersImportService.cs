using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Services.Shipping;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal static class XmlOffersImportService
    {
        private const string ProductTableName = "Product";

        internal static void Import(КоммерческаяИнформация source,
            IShippingService shippingService,
            IDbContext dbContext,
            string warehouseMappingsFile,
            string mappingsFile,
            string logFile)
        {

            var warehouseMappings = ImportWarehouses(source, shippingService, warehouseMappingsFile, logFile);

            ImportOffers(source, dbContext, warehouseMappings, mappingsFile, logFile);
        }

        private static void ImportOffers(КоммерческаяИнформация source,
            IDbContext dbContext,
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

            var sqlBuilder = new StringBuilder();

            foreach (var offer in source.ПакетПредложений.Предложения)
            {
                // только те продукты, которые были ранее добавлены
                if (!mappings.ContainsKey(offer.Ид)) continue;

                var productId = mappings[offer.Ид];

                //todo: скорее всего их будет потом несколько
                var whs = new[] { offer.Склад }.ToList();
                var whId = 0;
                var quantity = 0;
                //todo: пока поддерживаем только 1 склад, потом нужно будет написать отдельный код для нескольких складов
                if (whs.Count > 0)
                {
                    var wh = whs[0];
                    whId = warehouseMappings.ContainsKey(wh.ИдСклада) ? warehouseMappings[wh.ИдСклада] : 0;
                    quantity = wh.КоличествоНаСкладе;
                }

                decimal price = 0;
                if (sitePrice != null)
                {
                    var offerPrice = new[] { offer.Цены.Цена }.ToList().FirstOrDefault(p => p.ИдТипаЦены == sitePrice.Ид);
                    if (offerPrice != null)
                    {
                        price = offerPrice.ЦенаЗаЕдиницу;
                    }
                }

                stats[0]++;

                sqlBuilder.AppendLine($"UPDATE [{ProductTableName}] SET [WarehouseId]={whId}, [StockQuantity]={quantity}, [Price]={price} WHERE [Id]={productId};");

                // выполняем обновление БД пакетами по <packageSize> штук
                const int packageSize = 1000;
                if (stats[0] % packageSize == 0)
                {
                    try
                    {
                        dbContext.ExecuteSqlCommand(sqlBuilder.ToString(), true);
                        sqlBuilder.Clear();
                    }
                    catch (Exception ex)
                    {
                        logFile.Log($"Ошибка при обновлении пакета предложений: {stats[0] / packageSize}. {ex}");
                    }
                }
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