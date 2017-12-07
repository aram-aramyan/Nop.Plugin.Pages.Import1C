using Newtonsoft.Json;
using Nop.Services.Catalog;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;
using Nop.Core.Domain.Catalog;
using Nop.Services.Seo;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal class XmlManufacturerImportService
    {
        internal static List<Manufacturer> Import(КоммерческаяИнформация source,
            IManufacturerService manufacturerService,
            IUrlRecordService urlRecordService,
            string mappingsFile,
            out Dictionary<string, int> outMappings,
            string logFile)
        {
            logFile.Log("Начало импорта производителей");
            var stats = new[] { 0, 0 };

            var mappings = File.Exists(mappingsFile)
                ? JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(mappingsFile))
                : new Dictionary<string, int>();

            var manufacturers = manufacturerService.GetAllManufacturers(showHidden: true).ToList();

            var sourceManufacturers = source.Каталог.Товары
                .Where(t => t.Изготовитель != null)
                .Select(t => new { Id = t.Изготовитель.Ид, Name = t.Изготовитель.Наименование })
                .Distinct()
                .ToList();

            foreach (var man in sourceManufacturers)
            {
                var manufacturer = mappings.ContainsKey(man.Id)
                    ? manufacturers.FirstOrDefault(m => m.Id == mappings[man.Id])
                    : null;

                if (manufacturer == null)
                {
                    manufacturer = manufacturers.FirstOrDefault(m => m.Name == man.Name && !mappings.ContainsValue(m.Id));
                    if (manufacturer == null)
                    {
                        manufacturer = new Manufacturer
                        {
                            Name = man.Name,
                            CreatedOnUtc = DateTime.UtcNow,
                            UpdatedOnUtc = DateTime.UtcNow,
                            Published = true,
                            PageSize = 6,
                            PageSizeOptions = "6, 3, 9",
                            AllowCustomersToSelectPageSize = true,
                            ManufacturerTemplateId = 1
                        };
                        manufacturerService.InsertManufacturer(manufacturer);
                        var seName = manufacturer.ValidateSeName(null, manufacturer.Name, true);
                        urlRecordService.SaveSlug(manufacturer, seName, 0);
                        manufacturers.Add(manufacturer);
                        logFile.Log($"Новый бренд {manufacturer.Name} ({manufacturer.Id}): {man.Id}");
                        stats[0]++;
                    }
                    else
                    {
                        logFile.Log($"Существующий бренд {manufacturer.Name} ({manufacturer.Id}): {man.Id}");
                        stats[1]++;
                    }
                    mappings[man.Id] = manufacturer.Id;
                }
            }

            File.WriteAllText(mappingsFile, JsonConvert.SerializeObject(mappings, Formatting.Indented), Encoding.UTF8);
            logFile.Log($"Импорт производителей завершен. Привязано: {stats[1]}. Добавлено: {stats[0]}.");

            outMappings = mappings;
            return manufacturers;
        }
    }
}
