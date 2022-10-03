using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Newtonsoft.Json;

namespace Prescribing_System.Controllers
{
    public class AccountController : Controller
    {
        public GlobalDbContext Data = new GlobalDbContext();
        //VERIFY IF A USER IS LOGGED IN THEN REDIRECTS THEM TO THEIR DESIGNATED
        //HOME PAGE IN THEIR AREA
        //OTHERWISE REDIRECT THEM TO THE LOGIN PAGE
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
        [HttpGet]
        public IActionResult RegisterView(RegisterViewModel model)
        {
            var session = new MySession(HttpContext.Session);
            if (UserIsVerified(session.GetRole()))
                return RedirectToAction("Index", "Home",
                            new { area = session.GetRole() });
            else
            {
                ViewBag.Provinces = model.Provinces;
                ViewBag.Prov_s = JsonConvert.SerializeObject(model.Provinces);
                ViewBag.Cities = model.Cities;
                ViewBag.City_s = JsonConvert.SerializeObject(model.Cities);
                ViewBag.Suburbs = model.Suburbs;
                ViewBag.Suburb_s = JsonConvert.SerializeObject(model.Suburbs);
                if (model.UserPatient == null)
                    return View("Register");
                return View("Register", model);
            }
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            //CODE TO REGISTER A USER TO THE USER TABLE AND THE PATIENT TABLE
            //AND LOGS THE USER IN
            if (Data.CheckEmail(model.UserPatient.EmailAddress))
                ModelState.AddModelError("UserPatient.EmailAddress", "Email already in use.");
            if (Data.CheckID(model.UserPatient.IdNumber))
                ModelState.AddModelError("UserPatient.IdNumber", "ID number registered");
            if (ModelState.IsValid)
            {
                
                bool isAdded = Data.AddUser(model);
                if (isAdded)
                {
                    LoginViewModel loginModel = new LoginViewModel()
                    {
                        Username = model.UserPatient.EmailAddress,
                        Password = model.UserDetails.Password
                    };
                    ViewBag.Message = "Account successfully created. You may proceed to login";
                    ViewBag.Area = "";
                    ViewBag.Ctrl = "Account";
                    ViewBag.Action = "Login";
                    return View("Acknowledgement");
                }
            }
            ModelState.AddModelError("", "Invalid information");
            return RegisterView(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            new MySession(HttpContext.Session).KillSession();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
