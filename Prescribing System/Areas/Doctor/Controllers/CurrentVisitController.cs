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
    public class CurrentVisitController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        [HttpGet]
        public IActionResult Add(CurrentDoctorVisit model, int id)
        {
            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(CurrentDoctorVisit model, int id, int doctorID)
        {
            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            model.VisitDate = DateTime.Now.ToString();
            model.DoctorID = UserSingleton.GetLoggedUser().UserId;
            if (ModelState.IsValid)
            {
                bool isAdded = DoctorDbContext.AddCurrentDoctorVist(model);
                if (isAdded)
                {
                    TempData["Message"] = "Current Visit Added Successfully";
                    return RedirectToAction("Prescription", "AddPrescription");
                }
            }
            ModelState.AddModelError("", "error");
            return View(model);
        }
    }
}
