using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;

namespace Prescribing_System.Controllers
{
    public class ValidationController : Controller
    {
        public GlobalDbContext Data = new GlobalDbContext();
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
        public JsonResult CheckEmail([FromQuery(Name = "UserPatient.EmailAddress")] string emailAddress)
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
