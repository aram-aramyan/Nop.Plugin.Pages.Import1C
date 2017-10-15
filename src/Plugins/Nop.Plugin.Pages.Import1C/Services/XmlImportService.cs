using Nop.Services.Catalog;

namespace Nop.Plugin.Pages.Import1C.Services
{
    public class XmlImportService 
    {
        public static void Import(КоммерческаяИнформация source, ICategoryService categoryService)
        {
            // add categories
            var categories = XmlCategoryImportService.Import(source, categoryService);

        }
    }
}
