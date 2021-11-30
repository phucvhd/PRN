using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess.Repository;
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
        public ActionResult Index(string sortOrder, string searchString, string notify, DateTime datetime, int? pageNumber)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");

                ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewBag.PidSortParm = sortOrder == "pid_inc" ? "pid_desc" : "pid_inc";
                ViewBag.CidSortParm = sortOrder == "cid_inc" ? "cid_desc" : "cid_inc";
                ViewBag.Notify = notify;
                var ReportsList = ReportRepository.GetReports().ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    pageNumber = 0;
                    ViewBag.Notify = null;
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
                        ReportsList.Sort((n1, n2) => Int32.Parse(n1.CompanyId.Substring(2)).CompareTo(Int32.Parse(n2.CompanyId.Substring(2))));
                        break;
                    case "pid_inc":
                        ReportsList.Sort((n1, n2) => Int32.Parse(n2.ProductId.Substring(1)).CompareTo(Int32.Parse(n1.ProductId.Substring(1))));
                        break;
                    case "cid_inc":
                        ReportsList.Sort((n1, n2) => Int32.Parse(n2.CompanyId.Substring(2)).CompareTo(Int32.Parse(n1.CompanyId.Substring(2))));
                        break;
                    default:
                        ReportsList = ReportsList.OrderBy(r => r.Id).ToList();
                        break;
                }

                //Dashboard statistic
                if (datetime.Year > 2000)
                {
                    pageNumber = 0;
                    ViewBag.Notify = null;
                    var filterdReports = ReportRepository.GetReportsByCreatedDate(datetime);
                    ReportsList = filterdReports.ToList();
                }
                ViewData["DashboardStatistic"] = ReportRepository.DashboardStatistic();

                //Paging
                var pageIndex = pageNumber ?? 0;
                if (pageIndex == 0) ViewBag.PreDisabled = "disabled";
                if ((pageIndex * 10 + 10) < ReportsList.Count()) ReportsList = ReportsList.GetRange(pageIndex * 10, 10);
                else
                {
                    ViewBag.NextDisabled = "disabled";
                    ReportsList = ReportsList.GetRange((pageIndex) * 10, ReportsList.Count() - (pageIndex) * 10);
                }
                ViewBag.PageIndex = pageIndex;

                return View(ReportsList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: ReportsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: ReportsController/Create
        public ActionResult Create()
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                ViewBag.newid = ReportRepository.ReportIdGenerate();
                //object pro = TempData.Peek("product");
                //Product product = JsonConvert.DeserializeObject((string) pro) as Product;
                //ViewData["product"] = product;
                Product product = JsonConvert.DeserializeObject<Product>((String)TempData.Peek("product"));
                ViewBag.proId = product.ProductId;
                ViewData["companyList"] = CompanyRepository.GetCompanies();
                ViewBag.userEmail = TempData.Peek("userEmail");
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: ReportsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Report Report)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        Report.CreatedDate = DateTime.Now;
                        Company company = CompanyRepository.GetCompanyByID(Report.CompanyId);
                        Product product = JsonConvert.DeserializeObject<Product>((String)TempData.Peek("product"));

                        Report.Quantity = product.Quantity;
                        Report.IsReceiver = company.IsReceiver;
                        Report.IsSupplier = company.IsSupplier;
                        //productRepository.InsertProduct(product);
                        ReportRepository.InsertReport(Report);
                        TempData.Remove("product");
                    }
                    return RedirectToAction(nameof(Index), "Products", new { notify = "Create successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(Report);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }
        /*
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
        }*/

        // GET: ReportsController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not do this function!";
                    return RedirectToAction("Logout", "Home");
                }
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: ReportsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Report Report)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not do this function!";
                    return RedirectToAction("Logout", "Home");
                }
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        public ActionResult Port(string productId, string message)
        {
            try
            {
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Port(Report Report)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        Report.CreatedDate = DateTime.Now;
                        Company company = CompanyRepository.GetCompanyByID(Report.CompanyId);
                        Product product = productRepository.GetProductByID(Report.ProductId);

                        Report.IsReceiver = company.IsReceiver;
                        Report.IsSupplier = company.IsSupplier;

                        if (Report.IsReceiver)
                        {
                            if (product.Quantity < Math.Abs(Report.Quantity))
                            {
                                //ViewBag.Message = "Export exceed product quantity (" + product.Quantity.ToString() + ") !!!";
                                return RedirectToAction(nameof(Port), new { productId = product.ProductId, message = "Export exceed product quantity (" + product.Quantity.ToString() + ") !!!" });
                            }
                            product.Quantity -= Report.Quantity;
                            productRepository.UpdateProduct(product);
                        }
                        if (Report.IsSupplier)
                        {
                            product.Quantity += Report.Quantity;
                            productRepository.UpdateProduct(product);
                        }
                        ReportRepository.InsertReport(Report);
                        return RedirectToAction(nameof(Index), "Products", new { notify = "Export/Import Successfully!" });
                    }
                    return RedirectToAction(nameof(Index), "Products", new { notify = "Export/Import Failed!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(Report);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }
            
        }
    }
}
