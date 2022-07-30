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
        public IActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                return Edit(model.UserId);
            }
            ModelState.AddModelError("", "Invalid value");
            return View(model);
        }
    }
}
