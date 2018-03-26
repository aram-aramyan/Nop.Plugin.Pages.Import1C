using Nop.Services.Catalog;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal static class XmlOffersImportService
    {
        internal static void Import(КоммерческаяИнформация source, 
            IProductService productService, 
            string mappingsFile, 
            string logFile)
        {
            logFile.Log("Начало импорта предложений");
            var stats = new[] { 0 };

            logFile.Log($"Импорт предложений завершен. Обновлено: {stats[0]}.");
        }
    }
}