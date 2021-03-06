﻿using System;
using Nop.Services.Catalog;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal class XmlSpecificationAttributesImportService
    {
        const string AttrTypeDictionary = "Справочник";
        const string AttrTypeString = "Строка";

        internal static List<SpecificationAttribute> Import(КоммерческаяИнформация source,
            ISpecificationAttributeService specificationAttributeService,
            string mappingsFile,
                        out Dictionary<string, int> outMappings,
string logFile)
        {
            logFile.Log("Начало импорта свойств");
            var stats = new[] { 0, 0, 0, 0 };

            // attribute: key = 1c id, value = nopcommerce id
            // attribute value: key = <1c attribute id>.<1c value id>, value = nopcommerce id
            var mappings = File.Exists(mappingsFile)
                ? JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(mappingsFile))
                : new Dictionary<string, int>(); ;

            var attributes = specificationAttributeService.GetSpecificationAttributes().ToList();

            foreach (var attr in source.Классификатор.Свойства)
            {
                // ищем соответствие только по маппингу
                // потому что может быть несколько атрибутов с одинаковым названием

                var attribute = mappings.ContainsKey(attr.Ид)
                    ? attributes.FirstOrDefault(a => a.Id == mappings[attr.Ид])
                    : null;
                if (attribute != null)
                {
                    // для существующего только добавляем новые значения
                    if (attr.ТипЗначений == AttrTypeDictionary)
                    {
                        foreach (var attrOption in attr.ВариантыЗначений)
                        {
                            var key = $"{attr.Ид}.{attrOption.ИдЗначения}";

                            var option = mappings.ContainsKey(key)
                                ? attribute.SpecificationAttributeOptions.FirstOrDefault(o => o.Id == mappings[key])
                                : null;

                            if (option == null)
                            {
                                // ищем по имени
                                option = attribute.SpecificationAttributeOptions.FirstOrDefault(o => o.Name == attrOption.Значение);
                                if (option == null)
                                {
                                    // добавляем новое значение
                                    option = new Core.Domain.Catalog.SpecificationAttributeOption
                                    {
                                        SpecificationAttributeId = attribute.Id,
                                        Name = attrOption.Значение
                                    };
                                    specificationAttributeService.InsertSpecificationAttributeOption(option);
                                    attribute.SpecificationAttributeOptions.Add(option);
                                    //logFile.Log($"В существующий атрибут {attribute.Name} ({attribute.Id}) добавлено значение {option.Name}");
                                    stats[0]++;
                                }
                                else
                                {
                                    // мапим существующее значение
                                    //logFile.Log($"В существующем атрибуте {attribute.Name} ({attribute.Id}) добавлено сопоставление для значения {option.Name}");
                                    stats[1]++;
                                }
                                mappings[key] = option.Id;
                            }
                        }
                    }
                    continue;
                }

                // new attribute
                attribute = new Core.Domain.Catalog.SpecificationAttribute
                {
                    Name = attr.Наименование
                };
                specificationAttributeService.InsertSpecificationAttribute(attribute);
                attributes.Add(attribute);
                mappings[attr.Ид] = attribute.Id;
                //logFile.Log($"Новый атрибут {attribute.Name} ({attribute.Id})");
                stats[2]++;

                if (attr.ТипЗначений == AttrTypeDictionary)
                {
                    foreach (var attrOption in attr.ВариантыЗначений)
                    {
                        var key = $"{attr.Ид}.{attrOption.ИдЗначения}";

                        var option = new Core.Domain.Catalog.SpecificationAttributeOption
                        {
                            SpecificationAttributeId = attribute.Id,
                            Name = attrOption.Значение
                        };
                        specificationAttributeService.InsertSpecificationAttributeOption(option);
                        attribute.SpecificationAttributeOptions.Add(option);

                        mappings[key] = option.Id;
                        //logFile.Log($"В новый атрибут {attribute.Name} ({attribute.Id}) добавлено значение {option.Name}");
                        stats[3]++;
                    }
                }
                else
                {
                    var key = $"{attr.Ид}.";
                    var option = new Core.Domain.Catalog.SpecificationAttributeOption
                    {
                        SpecificationAttributeId = attribute.Id,
                        Name = string.Empty
                    };
                    specificationAttributeService.InsertSpecificationAttributeOption(option);
                    attribute.SpecificationAttributeOptions.Add(option);

                    mappings[key] = option.Id;
                }
            }

            File.WriteAllText(mappingsFile, JsonConvert.SerializeObject(mappings, Formatting.Indented), Encoding.UTF8);
            logFile.Log($"Импорт свойств завершен. Привязано: {stats[0]} атрибутов и {stats[1]} значений. Добавлено: {stats[2]} атрибутов и {stats[3]} значений.");

            outMappings = mappings;
            return attributes;
        }
    }
}
