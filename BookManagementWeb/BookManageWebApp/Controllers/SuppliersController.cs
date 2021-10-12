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
    public class SuppliersController : Controller
    {
        ISupplierRepository supplierRepository = null;
        public SuppliersController() => supplierRepository = new SupplierRepository();
        // GET: SuppliersController
        public ActionResult Index()
        {
            var supplierList = supplierRepository.GetSuppliers();
            return View(supplierList);
        }

        // GET: SuppliersController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = supplierRepository.GetSupplierByID(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // GET: SuppliersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuppliersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    supplierRepository.InsertSupplier(supplier);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(supplier);
            }
        }

        // GET: SuppliersController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = supplierRepository.GetSupplierByID(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: SuppliersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    supplierRepository.UpdateSupplier(supplier);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(supplier);
            }
        }

        // GET: SuppliersController/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = supplierRepository.GetSupplierByID(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: SuppliersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    supplierRepository.DeleteSupplier(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(supplier);
            }
        }
    }
}
