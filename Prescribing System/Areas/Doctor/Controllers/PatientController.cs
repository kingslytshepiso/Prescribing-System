using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Doctor.Models;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class PatientController : Controller//Patient Controllers
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var model = DoctorDbContext.GetAllPatientsWithPaging(pageNumber, pageSize);
            if (UserIsVerified("Doctor"))
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
          
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
        public IActionResult PatientDiagnosis()
        {
             if (UserIsVerified("Doctor"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
