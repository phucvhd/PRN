using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;
using BookManagementLib.Repository;
using Newtonsoft.Json;

namespace BookManReportmentWeb.Models
{
    public class ReportsController : Controller
    {
        IReportRepository ReportRepository = null;
        ICompanyRepository CompanyRepository = new CompanyRepository();
        public ReportsController() => ReportRepository = new ReportRepository();
        // GET: ReportsController
        public ActionResult Index()
        {
            var ReportsList = ReportRepository.GetReports();
            return View(ReportsList);
        }

        // GET: ReportsController/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var Report = ReportRepository.GetReportByID(id);
            if (Report == null)
            {
                return NotFound();
            }
            return View(Report);
        }

        // GET: ReportsController/Create
        public ActionResult Create()
        {
            ViewBag.newid = ReportRepository.ReportIdGenerate();
            //object pro = TempData.Peek("product");
            //Product product = JsonConvert.DeserializeObject((string) pro) as Product;
            //ViewData["product"] = product;
            ViewBag.proId = TempData.Peek("product");
            ViewData["companyList"] = CompanyRepository.GetCompanies();
            ViewBag.userEmail = TempData.Peek("userEmail");
            return View();
        }

        // POST: ReportsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Report Report)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Report.CreatedDate = DateTime.Now;
                    ReportRepository.InsertReport(Report);
                }
                return RedirectToAction(nameof(Index),"Products");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Report);
            }
        }

        // GET: ReportsController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var Report = ReportRepository.GetReportByID(id);
            if (Report == null)
            {
                return NotFound();
            }
            return View(Report);
        }

        // POST: ReportsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Report Report)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ReportRepository.UpdateReport(Report);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Report);
            }
        }

        // GET: ReportsController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var Report = ReportRepository.GetReportByID(id);
            if (Report == null)
            {
                return NotFound();
            }
            return View(Report);
        }

        // POST: ReportsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Report Report)
        {
            try
            {
                ReportRepository.DeleteReport(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
