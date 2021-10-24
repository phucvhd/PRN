using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;
using BookManagementLib.Repository;

namespace BookManagementWeb.Models
{
    public class CompaniesController : Controller
    {
        ICompanyRepository CompanyRepository = null;
        public CompaniesController() => CompanyRepository = new CompanyRepository();
        // GET: CompanysController
        public ActionResult Index(string sortOrder, string searchString, string notify, int? pageNumber)
        {
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "name_inc" ? "name_desc" : "name_inc";
            ViewBag.EmailSortParm = sortOrder == "email_inc" ? "email_desc" : "email_inc";
            ViewBag.Notify = notify;
            var CompanyList = CompanyRepository.GetCompanies().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
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
            if ((pageIndex * 10 + 10) <= CompanyList.Count()) CompanyList = CompanyList.GetRange(pageIndex * 10, 10);
            else
            {
                ViewBag.NextDisabled = "disabled";
                CompanyList = CompanyList.GetRange((pageIndex) * 10, CompanyList.Count() - (pageIndex) * 10);
            }
            ViewBag.PageIndex = pageIndex;


            return View(CompanyList);
        }

        // GET: CompanysController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Company = CompanyRepository.GetCompanyByID(id);
            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }

        // GET: CompanysController/Create
        public ActionResult Create()
        {
            ViewBag.newid = CompanyRepository.CompanyIdGenerate();
            return View();
        }

        // POST: CompanysController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company Company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CompanyRepository.InsertCompany(Company);
                }
                return RedirectToAction(nameof(Index), new { notify = "Create success !" });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Company);
            }
        }

        // GET: CompanysController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Company = CompanyRepository.GetCompanyByID(id);
            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }

        // POST: CompanysController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Company Company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CompanyRepository.UpdateCompany(Company);
                }
                return RedirectToAction(nameof(Index), new { notify = "Update success !" });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Company);
            }
        }

        // GET: CompanysController/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Company = CompanyRepository.GetCompanyByID(id);
            if (Company == null)
            {
                return NotFound();
            }
            return View(Company);
        }

        // POST: CompanysController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Company Company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CompanyRepository.DeleteCompany(id);
                }
                return RedirectToAction(nameof(Index), new { notify = "Delete success !" });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Company);
            }
        }
    }
}
