using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagementLib.BusinessObject;

namespace BookManagementLib.DataAccess
{
    class ReportDAO
    {
        private static ReportDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ReportDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Report> GetReportList()
        {
            var Reports = new List<Report>();
            try
            {
                using var context = new BookManagementDBContext();
                Reports = context.Reports.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }

        public IEnumerable<Report> GetReportsBycompanyID(string companyID)
        {
            var Reports = new List<Report>();
            try
            {
                using var context = new BookManagementDBContext();
                Reports = context.Reports.Where(r => r.CompanyId.Equals(companyID)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }

        public IEnumerable<Report> GetReportsByProductID(string proID)
        {
            var Reports = new List<Report>();
            try
            {
                using var context = new BookManagementDBContext();
                Reports = context.Reports.Where(r => r.ProductId.Equals(proID)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }

        public Report GetReportByID(int id)
        {
            Report Report = null;
            try
            {
                using var context = new BookManagementDBContext();
                Report = context.Reports.SingleOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Report;
        }
        public void Insert(Report Report)
        {
            try
            {
                Report supRep = GetReportByID(Report.Id);
                if (supRep == null)
                {
                    using var context = new BookManagementDBContext();
                    context.Reports.Add(Report);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Report is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Report Report)
        {
            try
            {
                Report supRep = GetReportByID(Report.Id);
                if (supRep != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Reports.Update(Report);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Report does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int ReportID)
        {
            try
            {
                Report supRep = GetReportByID(ReportID);
                if (supRep != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Reports.Remove(supRep);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Report does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int IdGenerate()
        {
            int newid = 1;
            try
            {
                using var context = new BookManagementDBContext();
                Report report = context.Reports.ToList().OrderByDescending(r => r.Id).FirstOrDefault();
                if (report != null)
                {
                    newid = ++report.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newid;
        }

        public IEnumerable<Report> GetReportsByCreatedDate(DateTime Date)
        {
            var Reports = new List<Report>();
            try
            {
                Reports = GetReportList().Where(r => r.CreatedDate.Year == Date.Year
                                                      && r.CreatedDate.Month == Date.Month && r.CreatedDate.Date == Date.Date).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }
        public IEnumerable<Report> GetReportsForDates(DateTime StartDate, DateTime EndDate)
        {
            var Reports = new List<Report>();
            
            try
            {
                var reports = GetReportList();
                foreach (DateTime day in EachDay(StartDate, EndDate))
                {

                    List<Report> ReportList = reports.Where(r => r.CreatedDate.Year == day.Year
                                                          && r.CreatedDate.Month == day.Month && r.CreatedDate.Date == day.Date).ToList();
                    Reports.Concat(ReportList);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Reports;
        }
        public Tuple<int, int> DashboardStatistic()
        {
            var reports = GetReportList().Where(r=> r.CreatedDate.Month == DateTime.Now.Month && r.CreatedDate.Year == DateTime.Now.Year);
            int increase = 0;
            int decrease = 0;
            foreach (Report report in reports)
            {
                if (report.IsReceiver)
                {
                    decrease += Math.Abs(report.Quantity);
                }
                if (report.IsSupplier)
                {
                    increase += report.Quantity;
                }
            }
            return new Tuple<int, int>(increase, decrease);
        }
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
