using System.Web.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Configuration;
using Nop.Services.Stores;
using System.Web;

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

        public Import1CController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService)
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _storeService = storeService;
            _pictureService = pictureService;
            _settingService = settingService;
            _cacheManager = cacheManager;
            _localizationService = localizationService;
        }

        public ActionResult Index()
        {
            return View("~/Plugins/Pages.Import1C/Views/Import1C.cshtml");
        }
        
        public ActionResult Upload(HttpPostedFileBase uploadedFile)
        {
            return Content("Hello import");
        }
    }
}
