using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;

namespace Prescribing_System.Controllers
{
    public class AccountController : Controller
    {
        public GlobalDbContext Data = new GlobalDbContext();
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            var session = new MySession(HttpContext.Session);
            if(UserIsVerified(session.GetRole()))
                return RedirectToAction("Index", "Home",
                            new { area = session.GetRole() });
            else
                return View(model);
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                result = Data.Login(model.Username, model.Password);
                if (result)
                {
                    
                    User user = Data.GetUserWithUsername(model.Username);
                    var session = new MySession(HttpContext.Session);
                    session.SetUser(user.UserId.ToString(), user.Role);
                    if (UserIsVerified(session.GetRole()))
                    {
                        UserSingleton.LogUser(user);
                        return RedirectToAction("Index", "Home",
                            new { area = session.GetRole() });
                    }
                    else
                        return Content("User not authorized");
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            new MySession(HttpContext.Session).KillSession();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
