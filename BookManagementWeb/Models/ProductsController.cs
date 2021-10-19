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
        public ActionResult Index(string sortOrder, string searchString, string notify, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "name_inc" ? "name_desc" : "name_inc";
            ViewBag.Notify = notify;
            var productList = productRepository.GetProducts().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                productList = productList.Where(p => p.ProductId.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || p.Isbn10.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || p.Isbn13.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            switch (sortOrder)
            {
                case "id_desc":
                    productList.Sort((n1, n2) => Int32.Parse(n2.ProductId.Substring(1)).CompareTo(Int32.Parse(n1.ProductId.Substring(1))));
                    break;
                case "name_desc":
                    productList.Sort((n1, n2) => n2.ProductName.CompareTo(n1.ProductName));
                    break;
                case "name_inc":
                    productList.Sort((n1, n2) => n1.ProductName.CompareTo(n2.ProductName));
                    break;
                default:
                    productList.Sort((n1, n2) => Int32.Parse(n1.ProductId.Substring(1)).CompareTo(Int32.Parse(n2.ProductId.Substring(1))));
                    break;
            }

            //Paging
            var pageIndex = pageNumber ?? 0;
            if (pageIndex == 0) ViewBag.PreDisabled = "disabled";
            if ((pageIndex * 10 + 10) <= productList.Count()) productList = productList.GetRange(pageIndex * 10, 10);
            else {
                ViewBag.NextDisabled = "disabled";
                productList = productList.GetRange((pageIndex) * 10, productList.Count() - (pageIndex) * 10);
            }
            ViewBag.PageIndex = pageIndex;


            ViewBag.Stock = productRepository.GetStock();
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

                if (!productRepository.CheckISBN(product.Isbn10) || !productRepository.CheckISBN(product.Isbn13))
                {
                    ViewBag.Message = "ISBN available";
                    return View(product);
                }

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
                    //product.LastModified = DateTime.Now;
                    productRepository.UpdateProduct(product);
                }
                return RedirectToAction(nameof(Index), new { notify = "Update success !" });
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
