using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Users;
using Prescribing_System.Areas.Admin.Models.System_Objects;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public AdminDbContext Data = new AdminDbContext();
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        //[Route("[area]/[controller]s/Page/{pageNumber?}")]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var model = Data.GetAllUsersWithPaging(pageNumber, pageSize);
            if (UserIsVerified("Admin"))
                return View(model);
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = Data.GetUserWithId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdateUser(user);
                return RedirectToAction("Edit","User", user.UserId);
            }
            ModelState.AddModelError("", "Invalid value");
            return View(user);
        }
        [HttpGet]
        public IActionResult Add(string type)
        {
            ViewBag.UserType = type;
            var model = new object();
            if (type == "Doctor")
            {
                model = new AddUserViewModel()
                {
                    SelectedUser = new DoctorUser(),
                };
                ViewBag.MedPracs = Data.GetAllMedPracs();
                
            }
            if (type == "Pharmacist")
            {
                model = new AddUserViewModel()
                {
                    SelectedUser = new PharmacistUser(),
                };
                ViewBag.Pharmacies = Data.GetAllPharmacies();
            }
            if (type == "Patient")
            {
                model = new AddUserViewModel()
                {
                    SelectedUser = new PatientUser(),
                };
            }
            return View(model);
        }
       
    }
}
