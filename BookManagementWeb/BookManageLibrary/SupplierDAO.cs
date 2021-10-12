using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManageLibrary.DataAccess;

namespace BookManageLibrary
{
    class SupplierDAO
    {
        private static SupplierDAO instance = null;
        private static readonly object instanceLock = new object();
        public static SupplierDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SupplierDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Supplier> GetSupplierList()
        {
            var Suppliers = new List<Supplier>();
            try
            {
                using var context = new BookManagementDBContext();
                Suppliers = context.TblSuppliers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Suppliers;
        }

        public Supplier GetSupplierByID(string SupplierID)
        {
            Supplier Supplier = null;
            try
            {
                using var context = new BookManagementDBContext();
                Supplier = context.TblSuppliers.SingleOrDefault(m => m.SupplierId == SupplierID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Supplier;
        }

        public Supplier GetSupplierByName(string SupplierName)
        {
            Supplier Supplier = null;
            try
            {
                using var context = new BookManagementDBContext();
                Supplier = context.TblSuppliers.SingleOrDefault(m => m.SupplierName == SupplierName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Supplier;
        }

        public void Insert(Supplier Supplier)
        {
            try
            {
                Supplier sup = GetSupplierByID(Supplier.SupplierId);
                if (sup == null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblSuppliers.Add(Supplier);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Supplier is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Supplier Supplier)
        {
            try
            {
                Supplier sup = GetSupplierByID(Supplier.SupplierId);
                if (sup != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblSuppliers.Update(Supplier);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Supplier does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string SupplierID)
        {
            try
            {
                Supplier Supplier = GetSupplierByID(SupplierID);
                if (Supplier != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblSuppliers.Remove(Supplier);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Supplier does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
