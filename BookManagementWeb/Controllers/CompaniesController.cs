using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess.Repository;

namespace BookManagementWeb.Models
{
    public class CompaniesController : Controller
    {
        ICompanyRepository companyRepository = null;
        public CompaniesController() => companyRepository = new CompanyRepository();
        // GET: CompanysController
        public ActionResult Index(string sortOrder, string searchString, string notify, int? pageNumber)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewBag.NameSortParm = sortOrder == "name_inc" ? "name_desc" : "name_inc";
                ViewBag.EmailSortParm = sortOrder == "email_inc" ? "email_desc" : "email_inc";
                ViewBag.Notify = notify;
                var CompanyList = companyRepository.GetCompanies().ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    pageNumber = 0;
                    ViewBag.Notify = null;
                    CompanyList = CompanyList.Where(cm => cm.CompanyId.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                           || cm.CompanyName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                           || cm.CompanyEmail.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                           || cm.CompanyPhone.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                switch (sortOrder)
                {
                    case "id_desc":
                        CompanyList.Sort((n1, n2) => Int32.Parse(n2.CompanyId.Substring(2)).CompareTo(Int32.Parse(n1.CompanyId.Substring(2))));
                        break;
                    case "name_desc":
                        CompanyList.Sort((n1, n2) => n2.CompanyName.CompareTo(n1.CompanyName));
                        break;
                    case "name_inc":
                        CompanyList.Sort((n1, n2) => n1.CompanyName.CompareTo(n2.CompanyName));
                        break;
                    case "email_desc":
                        CompanyList.Sort((n1, n2) => n2.CompanyEmail.CompareTo(n1.CompanyEmail));
                        break;
                    case "email_inc":
                        CompanyList.Sort((n1, n2) => n1.CompanyEmail.CompareTo(n2.CompanyEmail));
                        break;
                    default:
                        CompanyList.Sort((n1, n2) => Int32.Parse(n1.CompanyId.Substring(2)).CompareTo(Int32.Parse(n2.CompanyId.Substring(2))));
                        break;
                }

                //Paging
                var pageIndex = pageNumber ?? 0;
                if (pageIndex == 0) ViewBag.PreDisabled = "disabled";
                if ((pageIndex * 10 + 10) < CompanyList.Count()) CompanyList = CompanyList.GetRange(pageIndex * 10, 10);
                else
                {
                    ViewBag.NextDisabled = "disabled";
                    CompanyList = CompanyList.GetRange((pageIndex) * 10, CompanyList.Count() - (pageIndex) * 10);
                }
                ViewBag.PageIndex = pageIndex;


                return View(CompanyList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CompanysController/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                if (id == null)
                {
                    return NotFound();
                }
                var Company = companyRepository.GetCompanyByID(id);
                if (Company == null)
                {
                    return NotFound();
                }
                return View(Company);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CompanysController/Create
        public ActionResult Create()
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                ViewBag.newid = companyRepository.CompanyIdGenerate();
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: CompanysController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company Company)
        {
            try
            {           
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        companyRepository.InsertCompany(Company);
                    }
                    return RedirectToAction(nameof(Index), new { notify = "Create successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(Company);
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CompanysController/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                if (id == null)
                {
                    return NotFound();
                }
                var Company = companyRepository.GetCompanyByID(id);
                if (Company == null)
                {
                    return NotFound();
                }
                return View(Company);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: CompanysController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Company Company)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        companyRepository.UpdateCompany(Company);
                    }
                    return RedirectToAction(nameof(Index), new { notify = "Update successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(Company);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CompanysController/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                if (id == null)
                {
                    return NotFound();
                }
                var Company = companyRepository.GetCompanyByID(id);
                if (Company == null)
                {
                    return NotFound();
                }
                return View(Company);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: CompanysController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Company Company)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    ReportRepository reportRepository = new ReportRepository();
                    var reports = new List<Report>();
                    reports = reportRepository.GetReportsByCompanyID(id).ToList();
                    if (reports.Any())
                    {
                        TempData["Message"] = "Company is binding with another report";
                        return RedirectToAction(nameof(Delete), new { id = id });
                    }
                    companyRepository.DeleteCompany(id);
                    return RedirectToAction(nameof(Index), new { notify = "Deleted! id:" + id });
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction(nameof(Delete), new { id = id });
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
