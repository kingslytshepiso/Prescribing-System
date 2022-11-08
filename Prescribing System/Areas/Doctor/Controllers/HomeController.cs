using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Doctor.Models;
using Newtonsoft.Json;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class HomeController : Controller
    {
        //CHECKS IF THE USER'S ROLE AGAINST THE ROLE IN THE CURRENT AREA
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        public IActionResult Index()
        {
            //GETS THE USER THAT'S STORED IN THE STATIC CLASS "UserSingleton"
            IndexViewModel model = new IndexViewModel() { LoggedUser = UserSingleton.GetLoggedUser() };
            if (UserIsVerified("Doctor"))
                return View(model);
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        public DoctorDbContext Data = new DoctorDbContext();
        public IActionResult Profile()
        {
            if (UserIsVerified("Doctor"))
            {
                ViewBag.MedPracs = Data.GetAllMedPracs();
                ViewBag.Suburbs = Data.GetAllSuburbs();
                ViewBag.Suburb_s = JsonConvert.SerializeObject(Data.GetAllSuburbs());
                ViewBag.Cities = Data.GetAllCities();
                ViewBag.City_s = JsonConvert.SerializeObject(Data.GetAllCities());
                ViewBag.Provinces = Data.GetAllProvinces();
                ViewBag.Prov_s = JsonConvert.SerializeObject(Data.GetAllProvinces());
                var model = new UserUpdateViewModel();
                return View(model);
            }
            else
                return RedirectToAction("Login", "Account", new
                {
                    area = "",
                    returnUrl = this.Url
                });
        }
        public IActionResult Alerts()
        {
            var model = Data.GetAllPrescriptions();
            var lines = Data.GetAllPrescriptionLines();
            var tempLines = new List<PrescriptionLine>();
            model = model.FindAll(x => x.DoctorID == UserSingleton.GetLoggedUser().UserId)
                .FindAll(x => x.PrescrStatus == "Rejected");
            foreach (Prescription p in model)
            {
                foreach(PrescriptionLine l in lines)
                {
                    if(p.PrescriptionID == l.PrescriptionID)
                    {
                        tempLines.Add(l);
                    }
                }
            }
            ViewBag.Lines = tempLines;
            return View(model);
        }
    }
}
