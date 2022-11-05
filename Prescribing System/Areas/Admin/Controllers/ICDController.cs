using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Objects;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ICDController : Controller
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
        public IActionResult Index(int pageNumber = 1, int pageSize = 5, string sortBy = "none")
        {
            if (UserIsVerified("Admin"))
            {
                var model = Data.GetAllICDCodesWithPaging(pageNumber, pageSize, sortBy);
                switch (sortBy)
                {
                    case "code_a": model.DataList = model.DataList.OrderBy(x => x.Code).ToList();break;
                    case "code_d": model.DataList = model.DataList.OrderByDescending(x => x.Description).ToList();break;
                    case "desc_a": model.DataList = model.DataList.OrderBy(x => x.Description).ToList();break;
                    case "desc_d": model.DataList = model.DataList.OrderByDescending(x => x.Description).ToList();break;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ICD_Code model)
        {
            if (ModelState.IsValid)
            {
                bool result = Data.AddICDCode(model);
                if (result)
                {
                    TempData["Message"] = "Code added";
                    return RedirectToAction("Index", "ICD");
                }
            }
            ModelState.AddModelError("", "Invalid values.");
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = Data.GetAllICDCodes().Find(x => x.Code == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ICD_Code model)
        {
            if (ModelState.IsValid)
            {
                bool result = Data.UpdateICDCode(model);
                if (result)
                {
                    TempData["Message"] = "Code updated successfully";
                    return RedirectToAction("Index", "ICD");
                }
            }
            ModelState.AddModelError("", "Invalid values.");
            return View();
        }
    }
}
