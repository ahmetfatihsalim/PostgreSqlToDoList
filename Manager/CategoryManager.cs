using DataServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CategoryManager
    { // singleton
        private static CategoryManager _categoryManager;
        private static object lockObject = new object();
        CategoryService categoryService = new CategoryService();

        public CategoryManager()
        {

        }

        public static CategoryManager CreateAsCategoryManager()
        {
            lock (lockObject)
            {
                return _categoryManager ?? (_categoryManager = new CategoryManager());
            }
        }

        public List<Category> GetCategoryList()
        {
            return categoryService.GetList().Objects.ToList();
        }

        public string CategoryInsert(Category category)
        {
            return categoryService.Insert(category).ErrorText;
        }

        public Category GetCategoryById(int id)
        {
            return categoryService.GetById(id).Object;
        }
        public string UpdateCategory(Category category)
        {
            return categoryService.Update(category).ErrorText;
        }

    }
}
