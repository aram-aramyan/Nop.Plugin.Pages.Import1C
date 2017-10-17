using Nop.Services.Catalog;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Nop.Core.Domain.Catalog;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal class XmlCategoryImportService
    {
        //const string CategoryProperty = "Группа";
        const string CategoryItemsProperty = "Группы";
        const string CategoryNameProperty = "Наименование";
        const string CategoryIdProperty = "Ид";

        internal static Dictionary<string, int> Import(КоммерческаяИнформация source,
            ICategoryService categoryService,
            string mappingsFile,
            string logFile)
        {
            logFile.Log("Начало импорта категорий");

            var mappings = File.Exists(mappingsFile)
                ? JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(mappingsFile))
                : new Dictionary<string, int>(); ;

            var categories = categoryService.GetAllCategories(showHidden: true).Where(c => c != null).ToList();

            var rootCategory = source.Классификатор.Группы.Группа;

            var rootItem = GetCategoryItem(rootCategory);

            if (rootItem.Items != null)
                foreach (var child in rootItem.Items)
                    ImportCategory(child, null, categoryService, categories, mappings, logFile);

            File.WriteAllText(mappingsFile, JsonConvert.SerializeObject(mappings), Encoding.UTF8);
            logFile.Log("Импорт категорий завершен");

            return mappings;
        }

        static void ImportCategory(object categoryObject,
            CategoryItem parentItem,
            ICategoryService categoryService,
            List<Category> categories,
            Dictionary<string, int> mappings,
            string logFile)
        {
            var categoryItem = GetCategoryItem(categoryObject);
            //allCategories.Add(categoryItem);
            categoryItem.Parent = parentItem;

            // find by id in mappings
            if (mappings.ContainsKey(categoryItem.Id))
                categoryItem.MappedTo = categories.FirstOrDefault(c => c.Id == mappings[categoryItem.Id]);

            // by name
            if (categoryItem.MappedTo == null)
            {
                if (categoryItem.Parent == null)
                {
                    categoryItem.MappedTo = categories
                        .FirstOrDefault(c =>
                            c.Name.Equals(categoryItem.Name, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    categoryItem.MappedTo = categories
                        .FirstOrDefault(c => c.ParentCategoryId == categoryItem.Parent.MappedTo.Id
                            && c.Name.Equals(categoryItem.Name, StringComparison.InvariantCultureIgnoreCase));
                }

                if (categoryItem.MappedTo != null)
                {
                    mappings.Add(categoryItem.Id, categoryItem.MappedTo.Id);
                    logFile.Log($"Существующая категория {categoryItem.MappedTo.Name} ({categoryItem.MappedTo.Id}): {categoryItem.Id}");
                }
            }

            // not found - create new
            if (categoryItem.MappedTo == null)
            {
                var newCategory = new Category
                {
                    Name = categoryItem.Name,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    Published = true,
                    PageSize = 6,
                    PageSizeOptions = "6, 3, 9",
                    AllowCustomersToSelectPageSize = true
                };
                if (categoryItem.Parent != null)
                    newCategory.ParentCategoryId = categoryItem.Parent.MappedTo.Id;

                categoryService.InsertCategory(newCategory);

                categoryItem.MappedTo = newCategory;
                categories.Add(newCategory);

                mappings.Add(categoryItem.Id, categoryItem.MappedTo.Id);
                logFile.Log($"Новая категория {categoryItem.MappedTo.Name} ({categoryItem.MappedTo.Id}): {categoryItem.Id}");
            }

            if (categoryItem.Items != null)
                foreach (var child in categoryItem.Items)
                    ImportCategory(child, categoryItem, categoryService, categories, mappings, logFile);
        }

        static CategoryItem GetCategoryItem(object obj)
        {
            var type = GetCategoryType(obj);
            var item = new CategoryItem
            {
                Id = type.IdProperty?.GetValue(obj).ToString(),
                Name = type.NameProperty?.GetValue(obj).ToString(),
                Items = type.ItemsProperty == null ? null : type.ItemsProperty.GetValue(obj) as IEnumerable
            };

            return item;
        }

        internal class CategoryItem
        {
            public string Id;
            public string Name;
            public IEnumerable Items;
            public CategoryItem Parent;
            public Category MappedTo;
        }

        static Dictionary<string, CategoryType> _types = new Dictionary<string, CategoryType>();
        static CategoryType GetCategoryType(object obj)
        {
            var type = obj.GetType();
            if (!_types.ContainsKey(type.Name))
            {
                _types.Add(type.Name, new CategoryType
                {
                    IdProperty = type.GetProperty(CategoryIdProperty),
                    NameProperty = type.GetProperty(CategoryNameProperty),
                    ItemsProperty = type.GetProperty(CategoryItemsProperty)
                });
            }
            return _types[type.Name];
        }

        class CategoryType
        {
            public PropertyInfo IdProperty;
            public PropertyInfo NameProperty;
            public PropertyInfo ItemsProperty;
        }
    }
}
