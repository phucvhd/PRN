using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManageLibrary.DataAccess;

namespace BookManageLibrary
{
    class SuppliersReportDAO
    {
        private static SuppliersReportDAO instance = null;
        private static readonly object instanceLock = new object();
        public static SuppliersReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SuppliersReportDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<SuppliersReport> GetSuppliersReportList()
        {
            var SuppliersReports = new List<SuppliersReport>();
            try
            {
                using var context = new BookManagementDBContext();
                SuppliersReports = context.TblSuppliersReports.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return SuppliersReports;
        }

        public IEnumerable<SuppliersReport> GetSuppliersReportsBySupplierID(string supplierID)
        {
            var SuppliersReports = new List<SuppliersReport>();
            try
            {
                using var context = new BookManagementDBContext();
                SuppliersReports = context.TblSuppliersReports.Where(sr => sr.SuppilerId == supplierID).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return SuppliersReports;
        }

        public IEnumerable<SuppliersReport> GetSuppliersReportsByProductID(string proID)
        {
            var SuppliersReports = new List<SuppliersReport>();
            try
            {
                using var context = new BookManagementDBContext();
                SuppliersReports = (List<SuppliersReport>)context.TblSuppliersReports.OrderByDescending(sr => sr.ProductId == proID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return SuppliersReports;
        }

        public SuppliersReport GetSuppliersReportByID(int id)
        {
            SuppliersReport SuppliersReport = null;
            try
            {
                using var context = new BookManagementDBContext();
                SuppliersReport = context.TblSuppliersReports.SingleOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return SuppliersReport;
        }
        public void Insert(SuppliersReport SuppliersReport)
        {
            try
            {
                SuppliersReport supRep = GetSuppliersReportByID(SuppliersReport.Id);
                if (supRep == null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblSuppliersReports.Add(SuppliersReport);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The SuppliersReport is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(SuppliersReport SuppliersReport)
        {
            try
            {
                SuppliersReport supRep = GetSuppliersReportByID(SuppliersReport.Id);
                if (supRep != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblSuppliersReports.Update(SuppliersReport);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The SuppliersReport does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int SuppliersReportID)
        {
            try
            {
                SuppliersReport supRep = GetSuppliersReportByID(SuppliersReportID);
                if (supRep != null)
                {
                    using var context = new BookManagementDBContext();
                    context.TblSuppliersReports.Remove(supRep);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The SuppliersReport does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
