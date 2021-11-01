using BookManagementWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;
using BookManagementLib.Repository;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace BookManagementWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUserRepository userRepository = new UserRepository();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (TempData.Peek("userEmail") != null)
            {
                return RedirectToAction(nameof(Index), "Reports");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                bool status = userRepository.CheckLogin(user.Email, user.Password);
                if (status)
                {
                    TempData["userEmail"] = user.Email;
                    //HttpContext.Session.SetString("userEmail",user.Email);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Notify = "Wrong Email or Password";
                    return View();
                }
            }
            ViewBag.Notify = "Login Fail !!!";
            return View();
        }

        public ActionResult Logout()
        {
            TempData.Remove("userEmail");
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
