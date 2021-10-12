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
    public class UsersController : Controller
    {
        IUserRepository userRepository = null;
        public UsersController() => userRepository = new UserRepository();
        // GET: UsersController
        public ActionResult Index()
        {
            var userList = userRepository.GetUsers();
            return View(userList);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userRepository.InsertUser(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return View(nameof(Index));
            }
            return View(user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string email, User user)
        {
            try
            {
                if (email != user.Email)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    userRepository.UpdateUser(user);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(string email)
        {
            if (email == null)
            {
                return NotFound();
            }
            var user = userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string email, User user)
        {
            try
            {
                userRepository.DeleteUser(email);
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
