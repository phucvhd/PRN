using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess.Repository;
using System.Dynamic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace BookManagementWeb.Models
{
    public class ProductsController : Controller
    {
        IProductRepository productRepository = null;
        IAgeRepository ageRepository = new AgeRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();

        //public ProductsController() => productRepository = new ProductRepository();
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductsController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            productRepository = new ProductRepository();
        }


        // GET: ProductsController
        public ActionResult Index(string sortOrder, string searchString, string notify, int? pageNumber, string? CategoryID)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");

                ViewBag.CurrentSort = sortOrder;
                ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewBag.NameSortParm = sortOrder == "name_inc" ? "name_desc" : "name_inc";
                ViewBag.Notify = notify;
                ViewData["categories"] = categoryRepository.GetCategories();

                var productList = productRepository.GetProducts().ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    pageNumber = 0;
                    ViewBag.Notify = null;
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

                //Filter
                var categoryId = CategoryID ?? null;
                if (categoryId != null)
                {
                    pageNumber = 0;
                    ViewBag.Notify = null;
                    productList = productList.Where(p => p.CategoryId.Equals(categoryId)).ToList();
                }
                ViewBag.CategoryID = categoryId;

                //Paging
                var pageIndex = pageNumber ?? 0;
                if (pageIndex == 0) ViewBag.PreDisabled = "disabled";
                if ((pageIndex * 10 + 10) < productList.Count()) productList = productList.GetRange(pageIndex * 10, 10);
                else
                {
                    ViewBag.NextDisabled = "disabled";
                    productList = productList.GetRange((pageIndex) * 10, productList.Count() - (pageIndex) * 10);
                }
                ViewBag.PageIndex = pageIndex;


                ViewBag.Stock = productRepository.GetStock();
                ViewBag.Msg = TempData["msg"];
                return View(productList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: ProductsController/Details/5
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
                var product = productRepository.GetProductByID(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                ViewBag.newid = productRepository.ProductIdGenerate();
                ViewData["forAgeList"] = ageRepository.GetAges();
                ViewData["categoryList"] = categoryRepository.GetCategories();
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult Create(Product product)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                if (productRepository.CheckISBN(product.Isbn10) || productRepository.CheckISBN(product.Isbn13))
                {
                    ViewData["forAgeList"] = ageRepository.GetAges();
                    ViewData["categoryList"] = categoryRepository.GetCategories();
                    ViewBag.newid = productRepository.ProductIdGenerate();
                    ViewBag.Message = "Duplicate ISBN!";
                    return View(product);
                }

                if (ModelState.IsValid)
                {
                    product.CreatedDate = DateTime.Now;
                    product.LastModified = DateTime.Now;
                    productRepository.InsertProduct(product);
                    product.Image = "";
                    TempData["product"] = JsonConvert.SerializeObject(product);
                }
                return RedirectToAction(nameof(Create),"Reports");
            }
            catch (Exception ex)
            {
                ViewData["forAgeList"] = ageRepository.GetAges();
                ViewData["categoryList"] = categoryRepository.GetCategories();
                ViewBag.newid = productRepository.ProductIdGenerate();
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        // GET: ProductsController/Edit/5
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

                var product = productRepository.GetProductByID(id);
                if (product == null)
                {
                    return NotFound();
                }
                TempData["isbn10"] = product.Isbn10;
                TempData["isbn13"] = product.Isbn13;
                ViewData["forAgeList"] = ageRepository.GetAges();
                ViewData["categoryList"] = categoryRepository.GetCategories();
                return View(product);

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(int.MaxValue)]
        public ActionResult Edit(string id, Product product)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
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
                        if (!product.Isbn10.Equals(TempData["isbn10"]))
                        {
                            if (productRepository.CheckISBN(product.Isbn10))
                            {
                                ViewData["forAgeList"] = ageRepository.GetAges();
                                ViewData["categoryList"] = categoryRepository.GetCategories();
                                ViewBag.Message = "ISBN-10 Available";
                                return View(product);
                            }
                        }
                        if (!product.Isbn13.Equals(TempData["isbn13"]))
                        {
                            if (productRepository.CheckISBN(product.Isbn13))
                            {
                                ViewData["forAgeList"] = ageRepository.GetAges();
                                ViewData["categoryList"] = categoryRepository.GetCategories();
                                ViewBag.Message = "ISBN-13 Available";
                                return View(product);
                            }
                        }
                        product.LastModified = DateTime.Now;
                        productRepository.UpdateProduct(product);
                    }
                    return RedirectToAction(nameof(Index), new { notify = "Update successfully!" });
                }
                catch (Exception ex)
                {
                    TempData["isbn10"] = product.Isbn10;
                    TempData["isbn13"] = product.Isbn13;
                    ViewData["forAgeList"] = ageRepository.GetAges();
                    ViewData["categoryList"] = categoryRepository.GetCategories();
                    ViewBag.Message = ex.Message;
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not delete products!";
                    return RedirectToAction("Index");
                }

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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Product product)
        {
            try
            {
                //authentication
                if (TempData.Peek("userEmail") == null) return RedirectToAction("Login", "Home");
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["Msg"] = "Your role can not delete products!";
                    return RedirectToAction("Index");
                }
                try
                {
                    ReportRepository reportRepository = new ReportRepository();
                    var reports = new List<Report>();
                    reports = reportRepository.GetReportsByProductID(id).ToList();
                    if (reports.Any())
                    {
                        TempData["Message"] = "Product is binding with another report!" + reports.FirstOrDefault().ProductId;
                        return RedirectToAction(nameof(Delete), new { id = id });
                    }

                    productRepository.DeleteProduct(id);
                    return RedirectToAction(nameof(Index), new { notify = "Delete successfully!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
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
