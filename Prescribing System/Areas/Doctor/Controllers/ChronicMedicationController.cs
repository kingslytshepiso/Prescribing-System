using Microsoft.AspNetCore.Mvc;
using Prescribing_System.Areas.Doctor.Models;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class ChronicMedicationController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        [HttpGet]
        public IActionResult Add(PatientChronicMedicationModel model, int id, int pageNumber = 1, int pageSize = 5)
        {
            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            
            //model.List.Patient.PatientID = patientID;
            ViewBag.Medications = DoctorDbContext.GetChonicMedications();
            model = DoctorDbContext.GetAllPatientChronicMedications(id, pageNumber, pageSize);
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(PatientChronicMedicationModel model, int id)
        {
            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            if (ModelState.IsValid)
            {
                bool isAdded = DoctorDbContext.AddPatientChronicMedication(model);
                if (isAdded)
                {
                    TempData["Message"] = "Chronic medication added successfully";
                    return RedirectToAction("Add");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id,ChronicMedication model)
        {
            ViewBag.Medications = DoctorDbContext.GetAllMeds();
            model = DoctorDbContext.GetChronicMedicationById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ChronicMedication model, int id)
        {
            model.ChronicMedID = id;
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.UpdatePatientChronicMedication(model);
                if (result)
                {
                    TempData["Message"] = "Chronic medication updated successfully";
                    return RedirectToAction("Add");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id,ChronicMedication model)
        {
            model = DoctorDbContext.GetChronicMedicationById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.DeletePatientChronicMedication(id);
                if (result)
                {
                    TempData["Message"] = "Chronic medication deleted successfully";
                    return RedirectToAction("Add");
                }
            }
            TempData["Message"] = "Chronic medication deleted successfully";
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
