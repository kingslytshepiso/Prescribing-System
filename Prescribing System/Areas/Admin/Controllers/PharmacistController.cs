using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Users;
using System.Globalization;
using Newtonsoft.Json;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PharmacistController : Controller
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
                var model = Data.GetAllPharmacistsWithPaging(pageNumber, pageSize, sortBy);
                switch (sortBy)
                {
                    case "none":
                        model.DataList = model.DataList
                            .OrderBy(x => x.FirstName).ThenBy(x =>x.LastName).ToList(); break;
                    case "name":
                        model.DataList = model.DataList
                            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList(); break;
                    case "email":
                        model.DataList = model.DataList
                            .OrderBy(x => x.EmailAddress).ToList(); break;
                    case "pharmacy":
                        model.DataList = model.DataList
                            .OrderBy(x => x.PharmacyName).ToList(); break;
                    case "suburb":
                        model.DataList = model.DataList
                            .OrderBy(x => x.SuburbName).ToList(); break;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Pharmacies = Data.GetAllPharmacies();
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            return View();
        }
        [HttpPost]
        public IActionResult Add(PharmacistDataModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = Data.AddPharmacist(model);
                if (result)
                {
                    TempData["Message"] = "Pharmacist added.";
                    return RedirectToAction("Index", "Pharmacist");
                }
                ModelState.AddModelError("", "Error adding");
            }
            ViewBag.Pharmacies = Data.GetAllPharmacies();
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
            ViewBag.Pharmacies = Data.GetAllPharmacies();
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            var model = Data.GetPharmacistAndUserWithId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PharmacistDataModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdatePharmacist(model);
                if (result)
                {
                    TempData["Message"] = "Pharmacist user updated successfully";
                    return RedirectToAction("Index", "Pharmacist");
                }
                ModelState.AddModelError("", "Error Updating");
            }
            ModelState.AddModelError("", "Invalid values");
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (UserIsVerified("Admin"))
            {
                var result = Data.DeletePharmacist(id);
                if (result)
                {
                    TempData["Message"] = "Pharmacist removed";
                    return RedirectToAction("Index", "Pharmacist");
                }
                else
                {
                    TempData["Message"] = "Error removing";
                    return RedirectToAction("Index", "Pharmacist");
                }
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
