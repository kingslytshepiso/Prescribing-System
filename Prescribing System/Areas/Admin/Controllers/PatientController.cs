using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Users;
using Newtonsoft.Json;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        public AdminDbContext Data = new AdminDbContext();
        protected bool UserIsVerified(string role = "")
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
                var model = Data.GetAllPatientsWithPaging(pageNumber, pageSize, sortBy);
                switch (sortBy)
                {
                    case "none": model.DataList = model.DataList
                            .OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();break;
                    case "name_a": model.DataList = model.DataList
                            .OrderBy(x => x.IdNumber).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                            .ToList(); break;
                    case "name_d":
                        model.DataList = model.DataList
                        .OrderByDescending(x => x.IdNumber).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                        .ToList(); break;
                    case "id_a": model.DataList = model.DataList
                            .OrderBy(x => x.EmailAddress).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                            .ToList(); break;
                    case "id_d":
                        model.DataList = model.DataList
                        .OrderByDescending(x => x.EmailAddress).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                        .ToList(); break;
                    case "gender_a": model.DataList = model.DataList
                            .OrderBy(x => x.Gender).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                            .ToList(); break;
                    case "gender_d":
                        model.DataList = model.DataList
                        .OrderByDescending(x => x.Gender).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                        .ToList(); break;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            return View();
        }
        [HttpPost]
        public IActionResult Add(PatientDataModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = Data.AddPatient(model);
                if (result)
                {
                    TempData["Message"] = "Patient added.";
                    return RedirectToAction("Index", "Patient");
                }
                ModelState.AddModelError("", "Error adding");
            }
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            ModelState.AddModelError("", "Invalid information entered.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = Data.GetPatientAndUserWithId(id);
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PatientDataModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = Data.UpdatePatient(model);
                if (result)
                {
                    TempData["Message"] = "Patient Updated.";
                    return RedirectToAction("Index", "Patient");
                }
                ModelState.AddModelError("", "Error Updating");
            }
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            ModelState.AddModelError("", "Invalid information entered.");
            return View(model);
        }
    }
}
