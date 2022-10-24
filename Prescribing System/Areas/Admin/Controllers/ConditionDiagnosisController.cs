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
    public class ConditionDiagnosisController : Controller
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
        public IActionResult Index(int pageNumber = 1, int pageSize = 5,string sort = "none")
        {
            if (UserIsVerified("Admin"))
            {
                var model = Data.GetAllConditionDiagnosisWithPaging(pageNumber, pageSize, sort);
                switch (sort)
                {
                    case "none": break;
                    case "name": model.DataList = model.DataList.OrderBy(x => x.Code).ToList(); break;
                    case "description": model.DataList = model.DataList.OrderBy(x => x.Description).ToList(); break;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Codes = Data.GetAllICDCodes();
            ViewBag.Patients = Data.GetAllPatients();
            return View();
        }
        [HttpPost]
        public IActionResult Add(ConditionDiagnosis model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.AddConditionDiagnosis(model);
                if (result)
                {
                    TempData["Message"] = "Condition diagnosis added.";
                    return RedirectToAction("Index", "ConditionDiagnosis");
                }
                ModelState.AddModelError("", "Error adding.");
            }
            ViewBag.Codes = Data.GetAllICDCodes();
            ViewBag.Patients = Data.GetAllPatients();
            ModelState.AddModelError("", "Invalid values.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Codes = Data.GetAllICDCodes();
            ViewBag.Patients = Data.GetAllPatients();
            var model = Data.GetCondDiagnosisWithId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ConditionDiagnosis model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdateConditionDiagnosis(model);
                if (result)
                {
                    TempData["Message"] = "Condition diagnosis updated.";
                    return RedirectToAction("Index", "ConditionDiagnosis");
                }
                ModelState.AddModelError("", "Error updating.");
            }
            ViewBag.Codes = Data.GetAllICDCodes();
            ViewBag.Patients = Data.GetAllPatients();
            ModelState.AddModelError("", "Invalid values.");
            return View(model);
        }
    }
}
