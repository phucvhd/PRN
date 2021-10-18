using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;

namespace BookManagementLib
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
            var Categories = new List<Category>();
            try
            {
                using var context = new BookManagementDBContext();
                Categories = context.Categories.ToList();
                Categories.Sort((n1, n2) => Int32.Parse(n1.CategoryId.Substring(1)).CompareTo(Int32.Parse(n2.CategoryId.Substring(1))));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Categories;
        }

        public Category GetCategoryByID(string CategoryID)
        {
            Category Category = null;
            try
            {
                using var context = new BookManagementDBContext();
                Category = context.Categories.SingleOrDefault(m => m.CategoryId == CategoryID);
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
                Category = context.Categories.SingleOrDefault(m => m.CategoryName == CategoryName);
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
                    context.Categories.Add(Category);
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
                    context.Categories.Update(Category);
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
                    context.Categories.Remove(Category);
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

        public string IdGenerate()
        {
            string newid = null;
            try
            {
                using var context = new BookManagementDBContext();
                Category category = GetCategoryList().LastOrDefault();
                if (category == null)
                {
                    newid = "C1";
                }
                else
                {
                    newid = category.CategoryId.Substring(0, 1) + (Int32.Parse(category.CategoryId.Substring(1)) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newid;
        }
    }
}
