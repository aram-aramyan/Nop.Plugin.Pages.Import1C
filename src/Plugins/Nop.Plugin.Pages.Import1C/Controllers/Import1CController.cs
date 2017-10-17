﻿using System.Web.Mvc;
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
        private readonly ISpecificationAttributeService _specificationAttributeService;

        public Import1CController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService)
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _storeService = storeService;
            _pictureService = pictureService;
            _settingService = settingService;
            _cacheManager = cacheManager;
            _localizationService = localizationService;
            _categoryService = categoryService;
            _specificationAttributeService = specificationAttributeService;
        }

        public ActionResult Index()
        {
            return View("~/Plugins/Pages.Import1C/Views/Import1C.cshtml");
        }

        public ActionResult Upload(HttpPostedFileBase uploadedFile)
        {
            var dir = Request.MapPath("~/App_Data/Import1C");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var path = $"{dir}\\import-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xml";
            var logFile = $"{path}.log";

            uploadedFile.SaveAs(path);
            logFile.Log("Файл сохранен на сервер");

            var source = ReadXmlFile(path);
            logFile.Log("Файл успешно разобран");



            logFile.Log("Начало импорта");

            // add categories
            //var categories = XmlCategoryImportService.Import(
            //    source,
            //    _categoryService,
            //    $"{dir}\\CategoryMappings.json",
            //    logFile);

            // add attributes
            var attributes = XmlSpecificationAttributesImportService.Import(
                source,
                _specificationAttributeService,
                $"{dir}\\SpecificationAttributesMappings.json",
                logFile);

            logFile.Log("Импорт завершен");

            return File(logFile, "application/octet-stream", Path.GetFileName(logFile));
        }

        private static КоммерческаяИнформация ReadXmlFile(string path)
        {
            КоммерческаяИнформация result;
            var serializer = new XmlSerializer(typeof(КоммерческаяИнформация));
            using (var reader = new StreamReader(path))
            {
                result = (КоммерческаяИнформация)serializer.Deserialize(reader);
                reader.Close();
            }
            return result;
        }
    }
}
