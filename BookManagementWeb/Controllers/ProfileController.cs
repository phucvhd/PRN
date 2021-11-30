using BookManagementLib.BusinessObject;
using BookManagementLib.DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagementWeb.Controllers
{
    public class ProfileController : Controller
    {
        IUserRepository userRepository = new UserRepository();
        // GET: ProfileController
        public ActionResult Index()
        {
            try
            {
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 1)
                {
                    TempData["msg"] = "Your role can not do this function!";
                    return RedirectToAction("Logout", "Home");
                }
                var user = userRepository.GetUserByEmail(TempData.Peek("userEmail").ToString());
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(string email)
        {
            try
            {
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 1)
                {
                    TempData["msg"] = "Your role can not do this function!";
                    return RedirectToAction("Logout", "Home");
                }
                var user = userRepository.GetUserByEmail(TempData.Peek("userEmail").ToString());
                ViewBag.Message = TempData["msg"];
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string email, User user)
        {
            try
            {
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 1)
                {
                    TempData["msg"] = "Your role can not do this function!";
                    return RedirectToAction("Logout", "Home");
                }
                try
                {
                    if (ModelState.IsValid)
                    {
                        userRepository.UpdateUser(user);
                        return RedirectToAction(nameof(Index));
                    }
                    TempData["msg"] = "Something went wrong! Check your information!";
                    return RedirectToAction(nameof(Edit), new { email = email });

                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message;
                    return RedirectToAction(nameof(Edit), new { email = email });
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
