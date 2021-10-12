using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;
using BookManagementLib.Repository;

namespace BookManReportmentWeb.Models
{
    public class ReportsController : Controller
    {
        IReportRepository ReportRepository = null;
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
                    ReportRepository.InsertReport(Report);
                }
                return RedirectToAction(nameof(Index));
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
