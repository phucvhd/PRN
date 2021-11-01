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
    public class AgesController : Controller
    {
        IAgeRepository AgeRepository = null;
        public AgesController() => AgeRepository = new AgeRepository();
        // GET: AgesController
        public ActionResult Index(string sortOrder, string searchString, string notify, int? pageNumber)
        {
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.Notify = notify;
            var AgesList = AgeRepository.GetAges();
            if (!String.IsNullOrEmpty(searchString))
            {
                AgesList = AgesList.Where(a => a.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || a.ForAgesId.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    AgesList = AgesList.OrderByDescending(a => a.ForAgesId);
                    break;
                
                default:
                    AgesList = AgesList.OrderBy(a => a.ForAgesId);
                    break;
            }

            //Paging
            var pageIndex = pageNumber ?? 0;
            if (pageIndex == 0) ViewBag.PreDisabled = "disabled";
            if ((pageIndex * 10 + 10) < AgesList.Count()) AgesList = AgesList.ToList().GetRange(pageIndex * 10, 10);
            else
            {
                ViewBag.NextDisabled = "disabled";
                AgesList = AgesList.ToList().GetRange((pageIndex) * 10, AgesList.Count() - (pageIndex) * 10);
            }
            ViewBag.PageIndex = pageIndex;

            return View(AgesList);
        }

        // GET: AgesController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Age = AgeRepository.GetAgeByID(id);
            if (Age == null)
            {
                return NotFound();
            }
            return View(Age);
        }

        // GET: AgesController/Create
        public ActionResult Create()
        {
            string newid =AgeRepository.AgeIdGenerate();
            ViewBag.newid = newid;
            return View();
        }

        // POST: AgesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Age Age)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AgeRepository.InsertAge(Age);
                }
                return RedirectToAction(nameof(Index), new { notify = "Create success !" });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Age);
            }
        }

        // GET: AgesController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Age = AgeRepository.GetAgeByID(id);
            if (Age == null)
            {
                return NotFound();
            }
            return View(Age);
        }

        // POST: AgesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Age Age)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AgeRepository.UpdateAge(Age);
                }
                return RedirectToAction(nameof(Index),new { notify = "Update success !" });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(Age);
            }
        }

        // GET: AgesController/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Age = AgeRepository.GetAgeByID(id);
            if (Age == null)
            {
                return NotFound();
            }
            return View(Age);
        }

        // POST: AgesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Age Age)
        {
            try
            {
                AgeRepository.DeleteAge(id);
                return RedirectToAction(nameof(Index), new { notify = "Delete success !" });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
