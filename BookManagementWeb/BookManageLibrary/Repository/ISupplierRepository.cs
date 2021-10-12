using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
        Supplier GetSupplierByID(string supplierId);
        Supplier GetSupplierByName(string supplierName);
        void InsertSupplier(Supplier Supplier);
        void DeleteSupplier(string SuppliersId);
        void UpdateSupplier(Supplier Supplier);
    }
}
