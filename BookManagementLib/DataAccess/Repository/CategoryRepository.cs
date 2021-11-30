using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public Category GetCategoryByID(string categoryId) => CategoryDAO.Instance.GetCategoryByID(categoryId);
        public Category GetCategoryByName(string categoryName) => CategoryDAO.Instance.GetCategoryByName(categoryName);
        public IEnumerable<Category> GetCategories() => CategoryDAO.Instance.GetCategoryList();
        public void InsertCategory(Category category) => CategoryDAO.Instance.Insert(category);
        public void DeleteCategory(string categoryId) => CategoryDAO.Instance.Remove(categoryId);
        public void UpdateCategory(Category category) => CategoryDAO.Instance.Update(category);
        public string CategoryIdGenerate() => CategoryDAO.Instance.IdGenerate();
    }
}
