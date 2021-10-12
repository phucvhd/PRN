using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProductByID(string productId) => ProductDAO.Instance.GetProductByID(productId);
        public Product GetProductByName(string productName) => ProductDAO.Instance.GetProductByName(productName);
        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProductList();
        public void InsertProduct(Product Product) => ProductDAO.Instance.Insert(Product);
        public void DeleteProduct(string productId) => ProductDAO.Instance.Remove(productId);
        public void UpdateProduct(Product Product) => ProductDAO.Instance.Update(Product);
    }
}
