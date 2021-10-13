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
        public ActionResult Index()
        {
            var CompanyList = CompanyRepository.GetCompanies();
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Company);
            }
        }
    }
}
