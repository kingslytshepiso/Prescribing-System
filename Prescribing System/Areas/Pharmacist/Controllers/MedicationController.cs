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
    public class MedicationController : Controller
    {
        public PharmacistDbcontext Data = new PharmacistDbcontext();
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        public IActionResult Index(string keyword = "all", int pageNumber = 1,
            int pageSize = 10)
        {
            ListMedViewModel model = Data.SearchMedWithPaging(keyword, pageNumber,
                pageSize);
            if (UserIsVerified("Pharmacist"))
                return View(model);
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
