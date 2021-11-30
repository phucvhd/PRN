using BookManagementLib.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByID(string productID);
        Product GetProductByName(string productName);
        void InsertProduct(Product Product);
        void DeleteProduct(string ProductsId);
        void UpdateProduct(Product Product);
        string ProductIdGenerate();
        bool CheckISBN(string ISBN);
        int GetStock();
    }
}
