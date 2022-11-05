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
        public IActionResult Index(int pageNumber = 1, int pageSize = 5, 
            string sortBy = "none", string filterBy = "none")
        {
            if (UserIsVerified("Admin"))
            {
                var model = Data.GetAllUsersWithPaging(pageNumber, pageSize, sortBy, filterBy);
                switch (sortBy)
                {
                    case "none": model.DataList = model.DataList
                            .OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();break;
                    case "fname": model.DataList = model.DataList
                            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();break;
                    case "lname": model.DataList = model.DataList
                            .OrderBy(x => x.LastName).ToList(); break;
                    case "role": model.DataList = model.DataList
                            .OrderBy(x => x.Role).ThenBy(x => x.LastName).ToList();break;  
                }
                switch (filterBy)
                {
                    case "none": break;
                    case "doctor": model.DataList = model.DataList
                            .FindAll(x => x.Role == "Doctor");break;
                    case "patient": model.DataList = model.DataList
                            .FindAll(x => x.Role == "Patient");break;
                    case "pharmacist": model.DataList = model.DataList
                            .FindAll(x => x.Role == "Pharmacist");break;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
