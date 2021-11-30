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
    public class CategoriesController : Controller
    {
        ICategoryRepository categoryRepository = null;
        public CategoriesController() => categoryRepository = new CategoryRepository();
        // GET: CategoriesController
        public ActionResult Index(string sortOrder, string searchString, string notify, int? pageNumber)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewBag.NameSortParm = sortOrder == "name_inc" ? "name_desc" : "name_inc";
                ViewBag.Notify = notify;
                var categoriesList = categoryRepository.GetCategories().ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    pageNumber = 0;
                    ViewBag.Notify = null;
                    categoriesList = categoriesList.Where(c => c.CategoryId.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                           || c.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                switch (sortOrder)
                {
                    case "id_desc":
                        categoriesList.Sort((n1, n2) => Int32.Parse(n2.CategoryId.Substring(1)).CompareTo(Int32.Parse(n1.CategoryId.Substring(1))));
                        break;
                    case "name_desc":
                        categoriesList.Sort((n1, n2) => n2.CategoryName.CompareTo(n1.CategoryName));
                        break;
                    case "name_inc":
                        categoriesList.Sort((n1, n2) => n1.CategoryName.CompareTo(n2.CategoryName));
                        break;
                    default:
                        categoriesList.Sort((n1, n2) => Int32.Parse(n1.CategoryId.Substring(1)).CompareTo(Int32.Parse(n2.CategoryId.Substring(1))));
                        break;
                }

                //Paging
                var pageIndex = pageNumber ?? 0;
                if (pageIndex == 0) ViewBag.PreDisabled = "disabled";
                if ((pageIndex * 10 + 10) < categoriesList.Count()) categoriesList = categoriesList.GetRange(pageIndex * 10, 10);
                else
                {
                    ViewBag.NextDisabled = "disabled";
                    categoriesList = categoriesList.GetRange((pageIndex) * 10, categoriesList.Count() - (pageIndex) * 10);
                }
                ViewBag.PageIndex = pageIndex;

                return View(categoriesList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CategoriesController/Details/5
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
                var category = categoryRepository.GetCategoryByID(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                ViewBag.newid = categoryRepository.CategoryIdGenerate();
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        categoryRepository.InsertCategory(category);
                    }
                    return RedirectToAction(nameof(Index), new { notify = "Create successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    ViewBag.newid = categoryRepository.CategoryIdGenerate();
                    return View(category);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CategoriesController/Edit/5
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
                var category = categoryRepository.GetCategoryByID(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Category category)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    if (ModelState.IsValid)
                    {
                        categoryRepository.UpdateCategory(category);
                    }
                    return RedirectToAction(nameof(Index), new { notify = "Update successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(category);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: CategoriesController/Delete/5
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
                var category = categoryRepository.GetCategoryByID(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message;;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Category category)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                try
                {
                    ProductRepository productRepository = new ProductRepository();
                    var products = new List<Product>();
                    products = productRepository.GetProducts().Where(p => p.CategoryId.Equals(id)).ToList();
                    if (products.Any())
                    {
                        TempData["Message"] = "Category is binding with another Products ";
                        return RedirectToAction(nameof(Delete), new { id = id });
                    }

                    categoryRepository.DeleteCategory(id);
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
