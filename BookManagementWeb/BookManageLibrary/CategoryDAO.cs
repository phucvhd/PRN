using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManageLibrary.DataAccess;

namespace BookManageLibrary
{
    class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Category> GetCategoryList()
        {
            var Categorys = new List<Category>();
            try
            {
                using var context = new BookManagementDBContext();
                Categorys = context.TblCategories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Categorys;
        }

        public Category GetCategoryByID(string CategoryID)
        {
            Category Category = null;
            try
            {
                using var context = new BookManagementDBContext();
                Category = context.TblCategories.SingleOrDefault(m => m.CategoryId == CategoryID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Category;
        }

        public Category GetCategoryByName(string CategoryName)
        {
            Category Category = null;
            try
            {
                using var context = new BookManagementDBContext();
                Category = context.TblCategories.SingleOrDefault(m => m.CategoryName == CategoryName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Category;
        }

        public void Insert(Category Category)
        {
            try
            {
                Category cate = GetCategoryByID(Category.CategoryId);
                if (cate == null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblCategories.Add(Category);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Category is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Category Category)
        {
            try
            {
                Category cate = GetCategoryByID(Category.CategoryId);
                if (cate != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblCategories.Update(Category);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Category does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string CategoryID)
        {
            try
            {
                Category Category = GetCategoryByID(CategoryID);
                if (Category != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblCategories.Remove(Category);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Category does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
