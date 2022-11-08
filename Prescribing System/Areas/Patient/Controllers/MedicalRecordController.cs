using Microsoft.AspNetCore.Mvc;
using Prescribing_System.Models;
using Prescribing_System.Areas.Patient.Models;

namespace Prescribing_System.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class MedicalRecordController : Controller
    {
        public IActionResult Index()
        {
            int id = UserSingleton.GetLoggedUser().UserId;
            if (UserIsVerified("Patient"))
            {
                var model = new MedicalHistoryViewModel(id);
              
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
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

    }
}
