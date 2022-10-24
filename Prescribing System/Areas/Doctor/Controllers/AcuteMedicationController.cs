using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Prescribing_System.Areas.Doctor.Models;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class AcuteMedicationController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        [HttpGet]
        public IActionResult Add(PatientAcuteMedicationModel model, int id, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.Medications = DoctorDbContext.GetAcuteMedications();
            model.PatientID = id;
            model = DoctorDbContext.GetAllPatientAcuteMedications(id, pageNumber, pageSize);
            return View(model);
        }
        public IActionResult Add(PatientAcuteMedicationModel model, int id)
        {
            model.PatientID = id;
            if (ModelState.IsValid)
            {
                bool isAdded = DoctorDbContext.AddPatientAcuteMedication(model);
                if (isAdded)
                {
                    TempData["Message"] = "Acute medication added successfully";
                    return RedirectToAction("Add");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id, AcuteMedication model)
        {
            ViewBag.Medications = DoctorDbContext.GetAllMeds();
            model = DoctorDbContext.GetAcuteMedicationById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AcuteMedication model)
        {
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.UpdatePatientAcuteMedication(model);
                if (result)
                {
                    TempData["Message"] = "Acute medication updated successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.DeletePatientAcuteMedication(id);
                if (result)
                {
                    TempData["Message"] = "Acute medication deleted successfully";
                    return RedirectToAction("Add");
                }
            }
            TempData["Message"] = "Acute medication not deleted successfully";
            return RedirectToAction("Add");
        }
    }
}
