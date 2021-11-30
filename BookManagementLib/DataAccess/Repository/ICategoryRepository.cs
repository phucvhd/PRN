using BookManagementLib.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryByID(string categoryId);
        Category GetCategoryByName(string categoryName);
        void InsertCategory(Category category);
        void DeleteCategory(string CategorysId);
        void UpdateCategory(Category category);
        string CategoryIdGenerate();
    }
}
