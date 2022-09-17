using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Users;

namespace Prescribing_System.Areas.Admin.Controllers
{
    public class ValidationController : Controller
    {
        protected AdminDbContext Data = new AdminDbContext();
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult CheckID([FromQuery(Name = "UserPatient.IdNumber")] string IdNumber)
        {
            bool IdTaken = Data.CheckID(IdNumber);
            if (IdTaken)
            {
                return Json($"The ID Number {IdNumber} is registered to another user.");
            }
            else
                return Json(true);
        }
        public JsonResult CheckEmail([FromQuery(Name = "User.EmailAddress")] string emailAddress)
        {
            bool emailTaken = Data.CheckEmail(emailAddress);
            if (emailTaken)
            {
                return Json($"Email address {emailAddress} is already registered.");
            }
            else
                return Json(true);
        }
    }
}
