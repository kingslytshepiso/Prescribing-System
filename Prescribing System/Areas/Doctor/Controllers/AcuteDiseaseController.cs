using Microsoft.AspNetCore.Mvc;
using Prescribing_System.Areas.Doctor.Models;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class AcuteDiseaseController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        [HttpGet]
        public IActionResult Add(PatientAcuteDiseaseModel model,int id ,int pageNumber = 1, int pageSize = 5)
        {
            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            ViewBag.Diseases = DoctorDbContext.GetAllAcuteDiseasesD();
            model = DoctorDbContext.GetAllPatientAcuteDisease(id, pageNumber, pageSize);
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(PatientAcuteDiseaseModel model, int id)
        {
            model.PatientID = id;
            if (id == 0)
            {
                id = PatientModel.GetPatient().PatientID;
                model.PatientID = id;
            }
            if (ModelState.IsValid)
            {
                bool isAdded = DoctorDbContext.AddPatientAcuteDisease(model);
                if (isAdded)
                {
                    TempData["Message"] = "Acute disease Added Successfully";
                    return RedirectToAction("Add");
                }
            }
            ModelState.AddModelError("", "error");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id,AcuteDisease model)
        {
            ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
            model = DoctorDbContext.GetAcuteDiseaseById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AcuteDisease model)
        {
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.UpdatePatientAcuteDisease(model);
                if (result)
                {
                    TempData["Message"] = "Acute disease updated successfully";
                    return RedirectToAction("Add");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id,AcuteDisease model)
        {
            model = DoctorDbContext.GetAcuteDiseaseById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool result = DoctorDbContext.DeletePatientAcuteDisease(id);
                if (result)
                {
                    TempData["Message"] = "Acute disease deleted successfully";
                    return RedirectToAction("Add");
                }
            }
            TempData["Message"] = "Acute disease not deleted successfully";
            return RedirectToAction("Add");
        }
    }
}
