using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManageLibrary.DataAccess;
using BookManageLibrary.Repository;

namespace BookManageWebApp.Controllers
{
    public class CategoriesController : Controller
    {
        ICategoryRepository categoryRepository = null;
        public CategoriesController() => categoryRepository = new CategoryRepository();
        // GET: CategoriesController
        public ActionResult Index()
        {
            var categoriesList = categoryRepository.GetCategories();
            return View(categoriesList);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(string id)
        {
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

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryRepository.InsertCategory(category);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(category);
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(string id)
        {
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

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryRepository.UpdateCategory(category);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(category);
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(string id)
        {
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

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Category category)
        {
            try
            {
                categoryRepository.DeleteCategory(id);
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
