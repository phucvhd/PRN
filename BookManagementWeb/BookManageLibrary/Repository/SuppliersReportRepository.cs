using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    class SuppliersReportRepository : ISuppliersReportRepository
    {
        public SuppliersReport GetSuppliersReportByID(int suppliersreportId) => SuppliersReportDAO.Instance.GetSuppliersReportByID(suppliersreportId);
        public IEnumerable<SuppliersReport> GetSuppliersReportsBySupplierID(string supplierId) => SuppliersReportDAO.Instance.GetSuppliersReportsBySupplierID(supplierId);
        public IEnumerable<SuppliersReport> GetSuppliersReportsByProductID(string productId) => SuppliersReportDAO.Instance.GetSuppliersReportsByProductID(productId);
        public void InsertSuppliersReport(SuppliersReport SuppliersReport) => SuppliersReportDAO.Instance.Insert(SuppliersReport);
        public void DeleteSuppliersReport(int SuppliersReportId) => SuppliersReportDAO.Instance.Remove(SuppliersReportId);
        public void UpdateSuppliersReport(SuppliersReport SuppliersReport) => SuppliersReportDAO.Instance.Update(SuppliersReport);
    }
}
