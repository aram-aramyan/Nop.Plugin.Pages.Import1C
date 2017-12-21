using System.Web.Mvc;
using Nop.Web.Framework.Controllers;
using System.Web;
using System.Xml.Serialization;
using System.IO;
using System;
using Nop.Plugin.Pages.Import1C.Services;
using Nop.Services.Catalog;
using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Services.Media;
using Nop.Services.Seo;

namespace Nop.Plugin.Pages.Import1C.Controllers
{
    [AdminAuthorize]
    public class Import1CController : BasePluginController
    {
        private readonly ICategoryService _categoryService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IPictureService _pictureService;

        public Import1CController(ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IUrlRecordService urlRecordService,
            IPictureService pictureService
        )
        {
            _categoryService = categoryService;
            _specificationAttributeService = specificationAttributeService;
            _manufacturerService = manufacturerService;
            _productService = productService;
            _urlRecordService = urlRecordService;
            _pictureService = pictureService;
        }

        public ActionResult Index()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            return View("~/Plugins/Pages.Import1C/Views/Import1C.cshtml");
        }

        public ActionResult Upload(HttpPostedFileBase uploadedFile, 
            bool updateExisting = false,
            bool importSpecificationAttributes = false,
            bool overwriteCategories = false,
            bool overwriteManufacturers = false)
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
            Dictionary<string, int> categoryMappings;
            var categories = XmlCategoryImportService.Import(
                 source,
                 _categoryService,
                     _urlRecordService,
            $"{dir}\\CategoryMappings.json",
                 out categoryMappings,
                 logFile);

            // add attributes
            Dictionary<string, int> attributesMappings;
            List<SpecificationAttribute> attributes;
            if (importSpecificationAttributes)
            {
                attributes = XmlSpecificationAttributesImportService.Import(
                    source,
                    _specificationAttributeService,
                    $"{dir}\\SpecificationAttributesMappings.json",
                    out attributesMappings,
                    logFile);
            }
            else
            {
                attributesMappings = new Dictionary<string, int>();
                attributes = new List<SpecificationAttribute>();
            }

            // add manufacturers
            Dictionary<string, int> manufacturersMappings;
            var manufacturers = XmlManufacturerImportService.Import(
                source,
                _manufacturerService,
                  _urlRecordService,
              $"{dir}\\ManufacturerMappings.json",
                out manufacturersMappings,
                logFile);

            XmlCatalogImportService.Import(source,
                _categoryService,
                _specificationAttributeService,
                _manufacturerService,
                _productService,
                _urlRecordService,
                _pictureService,
                categories,
                categoryMappings,
                attributes,
                attributesMappings,
                manufacturers,
                manufacturersMappings,
                $"{dir}\\ProductsMappings.json",
                logFile, 
                new XmlCatalogImportService.ImportCatalogSettings
                {
                    UpdateExisting = updateExisting,
                    ImportSpecificationAttributes = importSpecificationAttributes,
                    OverwriteCategories = overwriteCategories,
                    OverwriteManufacturers = overwriteManufacturers
                });

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
