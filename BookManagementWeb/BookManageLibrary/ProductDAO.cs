using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManageLibrary.DataAccess;

namespace BookManageLibrary
{
    class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetProductList()
        {
            var Products = new List<Product>();
            try
            {
                using var context = new BookManagementDBContext();
                Products = context.TblProducts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Products;
        }

        public Product GetProductByID(string ProductID)
        {
            Product Product = null;
            try
            {
                using var context = new BookManagementDBContext();
                Product = context.TblProducts.SingleOrDefault(m => m.ProductId == ProductID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Product;
        }

        public Product GetProductByName(string ProductName)
        {
            Product Product = null;
            try
            {
                using var context = new BookManagementDBContext();
                Product = context.TblProducts.SingleOrDefault(m => m.ProductName == ProductName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Product;
        }

        //public Product GetProductByUnitPrice(int UnitPrice)
        //{
        //    Product Product = null;
        //    try
        //    {
        //        using var context = new BookManagementDBContext();
        //        Product = context.TblProducts.SingleOrDefault(m => m.UnitPrice == UnitPrice);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Product;
        //}

        //public Product GetProductByUnitInStock(int UnitsInStock)
        //{
        //    Product Product = null;
        //    try
        //    {
        //        using var context = new BookManagementDBContext();
        //        Product = context.TblProducts.SingleOrDefault(m => m.UnitsInStock == UnitsInStock);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Product;
        //}

        public void Insert(Product Product)
        {
            try
            {
                Product product = GetProductByID(Product.ProductId);
                if (product == null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblProducts.Add(Product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product Product)
        {
            try
            {
                Product product = GetProductByID(Product.ProductId);
                if (product != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblProducts.Update(Product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Product does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string ProductID)
        {
            try
            {
                Product product = GetProductByID(ProductID);
                if (product != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblProducts.Remove(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Product does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
