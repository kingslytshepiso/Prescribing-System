using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;
using Newtonsoft.Json;

namespace Prescribing_System.Areas.Pharmacist.Controllers
{
    [Area("Pharmacist")]
    public class HomeController : Controller
    {
        public PharmacistDbcontext Data = new PharmacistDbcontext();
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
            IndexViewModel model = new IndexViewModel() { 
                LoggedUser = UserSingleton.GetLoggedUser(),
                User = Data.GetPharmacistWithId(UserSingleton.GetLoggedUser().UserId),
                Pharmacy = Data.GetPharmacyWithId(UserSingleton.GetLoggedUser().UserId),
            };
            if (UserIsVerified("Pharmacist"))
                return View(model);
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
        }
        [HttpGet]
        public IActionResult Search(IndexViewModel model)
        {
            if (UserIsVerified("Pharmacist"))
            {
                if (ModelState.IsValid)
                {
                    var returnModel = new PatientViewModel(model.IdNumber);
                    returnModel.Prescriptions = returnModel.Prescriptions.OrderBy(x => x.Status).ToList();
                    returnModel.Prescriptions = returnModel.Prescriptions.FindAll(x => x.HasLines()).ToList();
                    if (returnModel.Patient == null)
                    {
                        ModelState.AddModelError("", "Patient not found");
                    }
                    else
                        return View("~/Areas/Pharmacist/Views/Patient/Index.cshtml", returnModel);
                }
                return View("Index", model);
            }
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
        }
        public IActionResult History(string sortBy = "none")
        {
            if (UserIsVerified("Pharmacist"))
            {
                var model = Data.GetPharmacistHistory();
                switch (sortBy)
                {
                    case "none":
                        model = model.OrderBy(x => x.Date).ToList(); break;
                    case "datenewest":
                        model = model.OrderByDescending(x => x.Date).ToList(); break;
                    case "dateoldest":
                        model = model.OrderBy(x => x.Date).ToList(); break;
                    case "action":
                        model = model.OrderBy(x => x.Action).ToList(); break;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Profile()
        {
            if (UserIsVerified("Pharmacist"))
            {
                ViewBag.Pharmacies = Data.GetAllPharmacies();
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
    }
}
