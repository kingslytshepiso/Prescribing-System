using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;

namespace Prescribing_System.Areas.Pharmacist.Controllers
{
    [Area("Pharmacist")]
    public class HomeController : Controller
    {
        public PharmacistDbcontext Data = new PharmacistDbcontext();
        //CHECKS IF THE USER'S ROLE AGAINST THE ROLE IN THE CURRENT AREA
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
            //GETS THE USER THAT'S STORED IN THE STATIC CLASS "UserSingleton"
            IndexViewModel model = new IndexViewModel() { 
                LoggedUser = UserSingleton.GetLoggedUser(),
                User = Data.GetPharmacistWithId(UserSingleton.GetLoggedUser().UserId),
                Pharmacy = Data.GetPharmacyWithId(UserSingleton.GetLoggedUser().UserId),
            };
            if (UserIsVerified("Pharmacist"))
                return View();
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
