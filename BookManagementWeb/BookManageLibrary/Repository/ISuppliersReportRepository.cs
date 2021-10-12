using BookManageLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManageLibrary.Repository
{
    public interface ISuppliersReportRepository
    {
        IEnumerable<SuppliersReport> GetSuppliersReportsBySupplierID(string supplierId);
        IEnumerable<SuppliersReport> GetSuppliersReportsByProductID(string productId);
        SuppliersReport GetSuppliersReportByID(int suppliersreportId);
        void InsertSuppliersReport(SuppliersReport suppliersreport);
        void DeleteSuppliersReport(int suppliersReportID);
        void UpdateSuppliersReport(SuppliersReport suppliersreport);
    }
}
