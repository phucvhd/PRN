using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        public Supplier GetSupplierByID(string supplierId) => SupplierDAO.Instance.GetSupplierByID(supplierId);
        public Supplier GetSupplierByName(string supplierName) => SupplierDAO.Instance.GetSupplierByName(supplierName);
        public IEnumerable<Supplier> GetSuppliers() => SupplierDAO.Instance.GetSupplierList();
        public void InsertSupplier(Supplier Supplier) => SupplierDAO.Instance.Insert(Supplier);
        public void DeleteSupplier(string supplierId) => SupplierDAO.Instance.Remove(supplierId);
        public void UpdateSupplier(Supplier Supplier) => SupplierDAO.Instance.Update(Supplier);
    }
}
