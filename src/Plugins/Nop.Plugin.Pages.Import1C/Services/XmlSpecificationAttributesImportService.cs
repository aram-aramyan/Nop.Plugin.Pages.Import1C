using System;
using Nop.Services.Catalog;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal class XmlSpecificationAttributesImportService
    {
        internal static object Import(КоммерческаяИнформация source,
            ISpecificationAttributeService specificationAttributeService,
            string logFile)
        {
            logFile.Log("Начало импорта свойств");



            logFile.Log("Импорт свойств завершен");

            return null;
        }
    }
}
