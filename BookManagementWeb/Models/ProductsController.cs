using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;
using BookManagementLib.Repository;
using System.Dynamic;
using Newtonsoft.Json;

namespace BookManagementWeb.Models
{
    public class ProductsController : Controller
    {
        IProductRepository productRepository = null;
        IAgeRepository ageRepository = new AgeRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();

        public ProductsController() => productRepository = new ProductRepository();
        // GET: ProductsController
        public ActionResult Index()
        {
            var productList = productRepository.GetProducts();
            return View(productList);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.newid = productRepository.ProductIdGenerate();
            ViewData["forAgeList"] = ageRepository.GetAges();
            ViewData["categoryList"] = categoryRepository.GetCategories();
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                ViewData["forAgeList"] = ageRepository.GetAges();
                ViewData["categoryList"] = categoryRepository.GetCategories();
                ViewBag.newid = productRepository.ProductIdGenerate();
                if (ModelState.IsValid)
                {
                    product.CreatedDate = DateTime.Now;
                    product.LastModified = DateTime.Now;
                    productRepository.InsertProduct(product);
                    TempData["product"] = product.ProductId;
                }
                return RedirectToAction(nameof(Create),"Reports");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productRepository.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["forAgeList"] = ageRepository.GetAges();
            ViewData["categoryList"] = categoryRepository.GetCategories();
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Product product)
        {
            try
            {
                ViewData["forAgeList"] = ageRepository.GetAges();
                ViewData["categoryList"] = categoryRepository.GetCategories();
                if (id != product.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    product.LastModified = DateTime.Now;
                    productRepository.UpdateProduct(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Product product)
        {
            try
            {
                productRepository.DeleteProduct(id);
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
