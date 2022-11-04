using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Doctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class ChronicDiseaseController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        [HttpGet]
        public IActionResult Add(PatientChronicDiseaseModel model, int id, int pageNumber = 1, int pageSize = 5)
        {

            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            ViewBag.Diseases = DoctorDbContext.GetAllChronicDiseasesD();
            model = DoctorDbContext.GetAllPatientChronicDisease(id, pageNumber, pageSize);
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(PatientChronicDiseaseModel model, int id)
        {
            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            if (ModelState.IsValid)
            {
                bool isAdded = DoctorDbContext.AddPatientChronicDisease(model);
                if (isAdded)
                {
                    TempData["Message"] = "Chronic disease Added Successfully";
                    return RedirectToAction("Add");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id, ChronicDisease model)
        {
            model = DoctorDbContext.GetAllChronicDiesase(id);
            ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
            return View(DoctorDbContext.GetAllChronicDiesase(id));
        }
        [HttpPost]
        public IActionResult Edit(ChronicDisease model, int id)
        {
            model.ChronicDiseaseID = id;
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.UpdatePatientChronicDisease(model);
                if (result)
                {
                    TempData["Message"] = "Chronic disease Updated Successfully";
                    return RedirectToAction("Add");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id, ChronicDisease model)
        {
            model = DoctorDbContext.GetAllChronicDiesase(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.DeletePatientChronicDisease(id);
                if (result)
                {
                    TempData["Message"] = "Chronic disease deleted Successfully";
                    return RedirectToAction("Add");
                }
            }
            TempData["Message"] = "Chronic disease not deleted Successfully";
            return RedirectToAction("Add");
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
    }
}
