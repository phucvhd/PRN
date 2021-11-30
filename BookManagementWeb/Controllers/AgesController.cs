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
    public class AgesController : Controller
    {
        IAgeRepository AgeRepository = null;
        public AgesController() => AgeRepository = new AgeRepository();
        // GET: AgesController
        public ActionResult Index(string sortOrder, string searchString, string notify, int? pageNumber)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewBag.Notify = notify;
                var AgesList = AgeRepository.GetAges();
                if (!String.IsNullOrEmpty(searchString))
                {
                    pageNumber = 0;
                    ViewBag.Notify = null;
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
            catch(Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: AgesController/Details/5
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
                var Age = AgeRepository.GetAgeByID(id);
                if (Age == null)
                {
                    return NotFound();
                }
                return View(Age);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: AgesController/Create
        public ActionResult Create()
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                string newid = AgeRepository.AgeIdGenerate();
                ViewBag.newid = newid;
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: AgesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Age Age)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        AgeRepository.InsertAge(Age);
                    }
                    return RedirectToAction(nameof(Index), new { notify = "Create successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    string newid = AgeRepository.AgeIdGenerate();
                    ViewBag.newid = newid;
                    return View(Age);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: AgesController/Edit/5
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
                var Age = AgeRepository.GetAgeByID(id);
                if (Age == null)
                {
                    return NotFound();
                }
                return View(Age);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: AgesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Age Age)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        AgeRepository.UpdateAge(Age);
                    }
                    return RedirectToAction(nameof(Index), new { notify = "Update successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(Age);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: AgesController/Delete/5
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
                var Age = AgeRepository.GetAgeByID(id);
                if (Age == null)
                {
                    return NotFound();
                }
                return View(Age);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: AgesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Age Age)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    ProductRepository productRepository = new ProductRepository();
                    var products = new List<Product>();
                    products = productRepository.GetProducts().Where(p => p.ForAgesId.Equals(id)).ToList();
                    if (products.Any())
                    {
                        TempData["Message"] = "This Age range is binding with another Products.";
                        return RedirectToAction(nameof(Delete), new { id = id });
                    }

                    AgeRepository.DeleteAge(id);
                    return RedirectToAction(nameof(Index), new { notify = "Delete successfully!" });
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction(nameof(Delete), new { id = id });
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }
    }
}
