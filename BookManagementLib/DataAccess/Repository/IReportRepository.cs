using BookManagementLib.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public interface IReportRepository
    {
        IEnumerable<Report> GetReportsByCompanyID(string supplierId);
        IEnumerable<Report> GetReportsByProductID(string productId);
        IEnumerable<Report> GetReports();
        Report GetReportByID(int ReportId);
        void InsertReport(Report Report);
        void DeleteReport(int ReportID);
        void UpdateReport(Report Report);
        int ReportIdGenerate();
        IEnumerable<Report> GetReportsByCreatedDate(DateTime Date);
        IEnumerable<Report> GetReportsForDates(DateTime StartDate, DateTime EndDate);
        Tuple<int, int> DashboardStatistic();

    }
}
