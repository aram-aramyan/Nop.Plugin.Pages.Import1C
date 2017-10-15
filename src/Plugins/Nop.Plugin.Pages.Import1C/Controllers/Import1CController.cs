using System.Web.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Configuration;
using Nop.Services.Stores;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System;
using Nop.Plugin.Pages.Import1C.Services;
using Nop.Services.Catalog;

namespace Nop.Plugin.Pages.Import1C.Controllers
{
    public class Import1CController : BasePluginController
    {
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly ICategoryService _categoryService;

        public Import1CController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            ICategoryService categoryService)
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _storeService = storeService;
            _pictureService = pictureService;
            _settingService = settingService;
            _cacheManager = cacheManager;
            _localizationService = localizationService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View("~/Plugins/Pages.Import1C/Views/Import1C.cshtml");
        }
        
        public ActionResult Upload(HttpPostedFileBase uploadedFile)
        {
            var path = Request.MapPath(string.Format("~/App_Data/import-{0:yyyy-MM-dd-HH-mm-ss}.xml", DateTime.Now));
            uploadedFile.SaveAs(path);

            var serializer = new XmlSerializer(typeof(КоммерческаяИнформация));
            var reader = new StreamReader(path);
            var result = (КоммерческаяИнформация)serializer.Deserialize(reader);
            reader.Close();

            XmlImportService.Import(result, _categoryService);

            return Content("Hello import");
        }
    }
}
