using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Objects;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PharmacyController : Controller
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
                var model = Data.GetAllPharmaciesWithPaging(pageNumber, pageSize, sortBy);
                if (model.DataList.Count > 0)
                {
                    switch (sortBy)
                    {
                        case "none": break;
                        case "name":
                            model.DataList = model.DataList
                                .OrderBy(x => x.Name).ToList(); break;
                        case "contact":
                            model.DataList = model.DataList
                                .OrderBy(x => x.ContactNo).ToList(); break;
                        case "email":
                            model.DataList = model.DataList
                                .OrderBy(x => x.EmailAddress).ToList(); break;
                        case "license":
                            model.DataList = model.DataList
                                .OrderBy(x => x.LicenseNo).ToList(); break;
                        case "suburb":
                            model.DataList = model.DataList
                                .OrderBy(x => x.SuburbName).ToList(); break;
                    }
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Pharmacists = Data.GetAllPharmacists();
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            return View();
        }
        [HttpPost]
        public IActionResult Add(Pharmacy model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.AddPharmacy(model);
                if (result)
                {
                    TempData["Message"] = "Pharmacy added.";
                    return RedirectToAction("Index", "Pharmacy");
                }
                ModelState.AddModelError("", "Error adding. A similar record may exist.");
            }
            ViewBag.Pharmacists = Data.GetAllPharmacists();
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            ModelState.AddModelError("", "Invalid values.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Pharmacists = Data.GetAllPharmacists();
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            var model = Data.GetAllPharmacies().Find(x => x.PharmacyId == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Pharmacy model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdatePharmacy(model);
                if (result)
                {
                    TempData["Message"] = "Pharmacy updated successfully";
                    return RedirectToAction("Index", "Pharmacy");
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
                var result = Data.DeletePharmacy(id);
                if (result)
                {
                    TempData["Message"] = "Pharmacy removed";
                    return RedirectToAction("Index", "Pharmacy");
                }
                else
                {
                    TempData["Message"] = "Error removing";
                    return RedirectToAction("Index", "Pharmacy");
                }
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
