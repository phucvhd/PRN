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
        IProductRepository productRepository = new ProductRepository();
        public ReportsController() => ReportRepository = new ReportRepository();
        // GET: ReportsController
        public ActionResult Index(string sortOrder, string searchString, string notify, DateTime datetime)
        {
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.PidSortParm = sortOrder == "pid_inc" ? "pid_desc" : "pid_inc";
            ViewBag.CidSortParm = sortOrder == "cid_inc" ? "cid_desc" : "cid_inc";
            ViewBag.Notify = notify;
            var ReportsList = ReportRepository.GetReports().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                ReportsList = ReportsList.Where(r => r.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || r.ProductId.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || r.CompanyId.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            switch (sortOrder)
            {
                case "id_desc":
                    ReportsList = ReportsList.OrderByDescending(r => r.Id).ToList();
                    break;
                case "pid_desc":
                    ReportsList.Sort((n1, n2) => Int32.Parse(n1.ProductId.Substring(1)).CompareTo(Int32.Parse(n2.ProductId.Substring(1))));
                    break;
                case "cid_desc":
                    ReportsList.Sort((n1, n2) => Int32.Parse(n1.CompanyId.Substring(1)).CompareTo(Int32.Parse(n2.CompanyId.Substring(1))));
                    break;
                case "pid_inc":
                    ReportsList.Sort((n1, n2) => Int32.Parse(n2.ProductId.Substring(1)).CompareTo(Int32.Parse(n1.ProductId.Substring(1))));
                    break;
                case "cid_inc":
                    ReportsList.Sort((n1, n2) => Int32.Parse(n2.CompanyId.Substring(1)).CompareTo(Int32.Parse(n1.CompanyId.Substring(1))));
                    break;

                default:
                    ReportsList = ReportsList.OrderBy(r => r.Id).ToList();
                    break;
            }

            //Dashboard statistic
            if (datetime.Year != 0001)
            {
                var filterdReports = ReportRepository.GetReportsByCreatedDate(datetime);
                ReportsList = filterdReports.ToList();
            }
            
            ViewData["DashboardStatistic"] = ReportRepository.DashboardStatistic();

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
                    Company company = CompanyRepository.GetCompanyByID(Report.CompanyId);
                    Product product = productRepository.GetProductByID(Report.ProductId);
                    Report.Quantity = product.Quantity;
                    Report.IsReceiver = company.IsReceiver;
                    Report.IsSupplier = company.IsSupplier;
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

        public ActionResult Port(string productId, string message)
        {
            Product product = productRepository.GetProductByID(productId);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.newid = ReportRepository.ReportIdGenerate();
            ViewBag.proId = product.ProductId;
            ViewData["companyList"] = CompanyRepository.GetCompanies();
            ViewBag.userEmail = TempData.Peek("userEmail");
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Port(Report Report)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    Report.CreatedDate = DateTime.Now;
                    Company company = CompanyRepository.GetCompanyByID(Report.CompanyId);
                    Product product = productRepository.GetProductByID(Report.ProductId);

                    if (Report.Quantity == 0)
                    {
                        return RedirectToAction(nameof(Port), new { productId = product.ProductId, message = "Quantity must be Integer excluding 0 !!!" });
                    }

                    if (Report.Quantity < 0) Report.IsReceiver = true;
                    if (Report.Quantity > 0) Report.IsSupplier = true;

                    if (Report.IsReceiver)
                    {
                        if (product.Quantity < Math.Abs(Report.Quantity))
                        {
                            //ViewBag.Message = "Export exceed product quantity (" + product.Quantity.ToString() + ") !!!";
                            return RedirectToAction(nameof(Port), new { productId = product.ProductId, message = "Export exceed product quantity (" + product.Quantity.ToString() + ") !!!" });
                        }
                        product.Quantity += Report.Quantity;
                        productRepository.UpdateProduct(product);
                    }
                    if (Report.IsSupplier)
                    {
                        product.Quantity += Report.Quantity;
                        productRepository.UpdateProduct(product);
                    }
                    ReportRepository.InsertReport(Report);
                    return RedirectToAction(nameof(Index), "Products", new { notify = "Export/Import success!"});
                }
                return RedirectToAction(nameof(Index), "Products", new { notify = "Export/Import Fail! /n " });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Report);
            }
        }
    }
}
