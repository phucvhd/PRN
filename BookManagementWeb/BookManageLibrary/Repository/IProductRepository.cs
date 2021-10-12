using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByID(string productID);
        Product GetProductByName(string productName);
        void InsertProduct(Product Product);
        void DeleteProduct(string ProductsId);
        void UpdateProduct(Product Product);
    }
}
