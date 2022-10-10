using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Doctor.Models;


namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class PrescriptionController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var model = DoctorDbContext.GetAllPatientsWithPaging(pageNumber, pageSize);
            if (UserIsVerified("Doctor"))
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

        }
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        [HttpGet]
        public IActionResult Add(int id)
        {
            if (UserIsVerified("Doctor"))
            {
                var model = DoctorDbContext.GetPatientWithId(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        [HttpPost]
        public IActionResult Add(int id, int DoctorId, string NowDate, PatientPrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                DoctorId = UserSingleton.GetLoggedUser().UserId;
                NowDate = DateTime.Today.ToString();
                bool isAdded = DoctorDbContext.AddPrescription(model, DoctorId, id, NowDate);
                if (isAdded)
                {
                    return RedirectToAction("AddPrescriptionLine");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddPrescription(int id)
        {
            if (UserIsVerified("Doctor"))
            {
                var model = DoctorDbContext.GetPatientWithId(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        [HttpPost]
        public IActionResult AddPrescription(int id,int DoctorId,string NowDate,PatientPrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                DoctorId = UserSingleton.GetLoggedUser().UserId;
                NowDate = DateTime.Today.ToString();
                bool isAdded = DoctorDbContext.AddPrescription(model,DoctorId,id,NowDate);
                if (isAdded)
                {
                    
                    return RedirectToAction("AddPrescriptionLine");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddPrescriptionLine(PatientPrescriptionViewModel model,int id,int doctorId, int LineID)
        {
            //var o = new PatientPrescriptionViewModel();
                ViewBag.Medications = model.Medications;
                ViewBag.DosageForms = model.Dosages;
            doctorId = UserSingleton.GetLoggedUser().UserId;
            //LineID = DoctorDbContext.GetPrescriptionById(id);
            model = DoctorDbContext.GetAllPresciptionLines(id, doctorId);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPrescriptionLine(PatientPrescriptionViewModel model,string quatity)
        {
            
            if (ModelState.IsValid)
            {
                model.PrescriptionLine.RepeatLeftNo = model.PrescriptionLine.RepeatNo;
                bool isAdded = DoctorDbContext.AddPrescriptionLine(model);
                if (isAdded)
                {
                    TempData["Message"] = "Line Added Successfully";
                    return RedirectToAction("AddPrescriptionLine");
                }
            }
            return View(model);
        }
    }
}
