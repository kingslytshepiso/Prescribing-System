using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public AdminDbContext Data = new AdminDbContext();
        public HomeController()
        {

        }
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel 
            { LoggedUser = UserSingleton.GetLoggedUser() };
            if (UserIsVerified("Admin"))
                return View(model);
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Search(string keyword)
        {
            var model = Data.SearchAll(keyword);
            var found = false;
            foreach (SearchIndexModel s in model)
            {
                if (s.Objects.Count > 0)
                    found = true;
            }
            ViewBag.Found = found;
            ViewBag.Keyword = keyword;
            return View(model);
        }
    }
}
