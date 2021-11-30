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
    public class UsersController : Controller
    {
        IUserRepository userRepository = null;
        public UsersController() => userRepository = new UserRepository();
        // GET: UsersController
        public ActionResult Index()
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not do this function!";
                    return RedirectToAction("Logout", "Home");
                }
                var userList = userRepository.GetUsers();
                return View(userList);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: UsersController/Details/5
        public ActionResult Details(string email)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not do this function!";
                    return RedirectToAction("Logout", "Home");
                }
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not add users!";
                    return RedirectToAction("Logout", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not add users!";
                    return RedirectToAction("Logout", "Home");
                }
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(string email)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not edit users!";
                    return RedirectToAction("Logout", "Home");
                }
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string email, User user)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not edit users!";
                    return RedirectToAction("Logout", "Home");
                }
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(string email)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not delete users!";
                    return RedirectToAction("Logout", "Home");
                }
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
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong! Logged out! Error: " + ex.Message; ;
                return RedirectToAction("Logout", "Home");
            }

        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string email, string confirm)
        {
            try
            {
                //authentication
                if (TempData.Peek("role") == null || (int)TempData.Peek("role") != 2)
                {
                    TempData["msg"] = "Your role can not delete users!";
                    return RedirectToAction("Logout", "Home");
                }
                try
                {
                    if (confirm != null && confirm.Equals("yes"))
                    {
                        ReportRepository reportRepository = new ReportRepository();
                        var reports = new List<Report>();
                        reports = reportRepository.GetReports().Where(r => r.UserEmail.Equals(email)).ToList();
                        if (reports.Any())
                        {
                            TempData["msg"] = "User is binding with another report!";
                            return RedirectToAction(nameof(Delete), new { email = email });
                        }

                        userRepository.DeleteUser(email);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["msg"] = "Something went wrong!!";
                        return RedirectToAction(nameof(Delete), new { email = email });
                    }

                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message;
                    return RedirectToAction(nameof(Delete), new { email = email });
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
