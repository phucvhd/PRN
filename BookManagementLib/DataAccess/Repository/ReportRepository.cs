using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementLib.DataAccess.Repository
{
    public class ReportRepository : IReportRepository
    {
        public IEnumerable<Report> GetReports() => ReportDAO.Instance.GetReportList();
        public Report GetReportByID(int ReportId) => ReportDAO.Instance.GetReportByID(ReportId);
        public IEnumerable<Report> GetReportsByCompanyID(string companyId) => ReportDAO.Instance.GetReportsBycompanyID(companyId);
        public IEnumerable<Report> GetReportsByProductID(string productId) => ReportDAO.Instance.GetReportsByProductID(productId);

        public void InsertReport(Report Report) => ReportDAO.Instance.Insert(Report);
        public void DeleteReport(int ReportId) => ReportDAO.Instance.Remove(ReportId);
        public void UpdateReport(Report Report) => ReportDAO.Instance.Update(Report);
        public int ReportIdGenerate() => ReportDAO.Instance.IdGenerate();

        public IEnumerable<Report> GetReportsByCreatedDate(DateTime Date) => ReportDAO.Instance.GetReportsByCreatedDate(Date);

        public IEnumerable<Report> GetReportsForDates(DateTime StartDate, DateTime EndDate) => ReportDAO.Instance.GetReportsForDates(StartDate, EndDate);

        public Tuple<int, int> DashboardStatistic() => ReportDAO.Instance.DashboardStatistic();
    }
}
