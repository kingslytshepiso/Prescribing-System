using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Newtonsoft.Json;
using Prescribing_System.Areas.Admin.Models.System_Objects;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicalPracticeController : Controller
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
                var model = Data.GetAllMedPracsWithPaging(pageNumber, pageSize, sortBy);
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
                        case "practiceno":
                            model.DataList = model.DataList
                                .OrderBy(x => x.PracticeNo).ToList(); break;
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
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            return View();
        }
        [HttpPost]
        public IActionResult Add(MedicalPractice model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.AddMedicalPractice(model);
                if (result)
                {
                    TempData["Message"] = "Medical Practice added.";
                    return RedirectToAction("Index", "Practice");
                }
                ModelState.AddModelError("", "Error adding. A similar record may exist.");
            }
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
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            var model = Data.GetAllMedPracs().Find(x => x.MedPracId == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(MedicalPractice model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdateMedicalPractice(model);
                if (result)
                {
                    TempData["Message"] = "Medical Practice updated successfully";
                    return RedirectToAction("Index", "MedicalPractice");
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
                var result = Data.DeleteMedPrac(id);
                if (result)
                {
                    TempData["Message"] = "Medical practice removed";
                    return RedirectToAction("Index", "MedicalPractice");
                }
                else
                {
                    TempData["Message"] = "Error removing";
                    return RedirectToAction("Index", "MedicalPractice");
                }
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
