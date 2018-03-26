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
            bool overwriteManufacturers = false,
            bool priceMode = false)
        {
            var dir = Request.MapPath("~/App_Data/Import1C");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var path = $"{dir}\\import-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xml";
            var logFile = $"{path}.log";

            uploadedFile.SaveAs(path);
            logFile.Log("Файл сохранен на сервер");

            var source = ReadXmlFile<КоммерческаяИнформация>(path);
            logFile.Log("Файл успешно разобран");

            logFile.Log("Начало импорта");

            if (priceMode)
            {
                ImportOffers(logFile, dir, source);
            }
            else
            {
                Import(updateExisting, importSpecificationAttributes, overwriteCategories, overwriteManufacturers, logFile, dir, source);
            }

            return File(logFile, "application/octet-stream", Path.GetFileName(logFile));
        }

        /// <summary>
        /// Импорт предложений
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="dir"></param>
        /// <param name="source"></param>
        private void ImportOffers(string logFile,
            string dir,
            КоммерческаяИнформация source)
        {
            XmlOffersImportService.Import(source,
                _productService,
                $"{dir}\\ProductsMappings.json",
                logFile);
        }

        /// <summary>
        /// Импорт
        /// </summary>
        /// <param name="updateExisting">обновлять существующие товары или игнорировать</param>
        /// <param name="importSpecificationAttributes">импортировать атрибуты</param>
        /// <param name="overwriteCategories">перезаписывать существующие категории или игнорировать</param>
        /// <param name="overwriteManufacturers">перезаписывать существующих производителей или игнорировать</param>
        /// <param name="logFile"></param>
        /// <param name="dir"></param>
        /// <param name="source"></param>
        private void Import(bool updateExisting,
            bool importSpecificationAttributes,
            bool overwriteCategories,
            bool overwriteManufacturers,
            string logFile,
            string dir, 
            КоммерческаяИнформация source)
        {
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
        }


        private static T ReadXmlFile<T>(string path)
        {
            T result;
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(path))
            {
                result = (T)serializer.Deserialize(reader);
                reader.Close();
            }
            return result;
        }
    }
}
