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
    public class DoctorController : Controller
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
                var model = Data.GetAllDoctorsWithPaging(pageNumber, pageSize, sortBy);
                if (model.DataList.Count > 0)
                {
                    switch (sortBy)
                    {
                        case "none": break;
                        case "name": model.DataList = model.DataList.OrderBy(x => x.LastName).ToList(); break;
                        case "suburb": model.DataList = model.DataList.OrderBy(x => x.SuburbName).ToList(); break;
                        case "medprac": model.DataList = model.DataList.OrderBy(x => x.MedPracName).ToList(); break;
                        case "status": model.DataList = model.DataList.OrderBy(x => x.StatusName).ToList(); break;
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
            ViewBag.MedPracs = Data.GetAllMedPracs();
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.MedPracs = Data.GetAllMedPracs();
            ViewBag.Suburbs = Data.GetAllSuburbs();
            ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
            ViewBag.Cities = Data.GetAllCities();
            ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
            ViewBag.Provinces = Data.GetAllProvinces();
            ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
            AddDoctorViewModel model = new AddDoctorViewModel()
            {
                User = Data.GetDoctorWithId(id),
                UserDetails = Data.GetUserWithId(id),
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.AddDoctor(model);
                if (result)
                {
                    TempData["Message"] = "Doctor successfully added.";
                    return RedirectToAction("Index", "Doctor");
                }
                ModelState.AddModelError("", "Error adding.");
            }
            ModelState.AddModelError("", "Invalid information entered.");
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AddDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdateDoctor(model);
                if (result)
                {
                    TempData["Message"] = "Doctor successfully updated.";
                    return RedirectToAction("Index", "Doctor");
                }
                ModelState.AddModelError("", "Error updating.");
            }
            ModelState.AddModelError("", "Invalid information entered.");
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (UserIsVerified("Admin"))
            {
                var result = Data.DeleteDoctor(id);
                if (result)
                {
                    TempData["Message"] = "Doctor user removed";
                    return RedirectToAction("Index", "Doctor");
                }
                else
                {
                    TempData["Message"] = "Error removing";
                    return RedirectToAction("Index", "Doctor");
                }
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
