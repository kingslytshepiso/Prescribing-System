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
    public class PatientController : Controller//Patient Controllers
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
        public IActionResult PatientDiagnosis(int patientID,PatientChronicDiseaseModel model, int pageNumber = 1, int pageSize = 10)
        {
            // if (UserIsVerified("Doctor"))
            //{
            //    ViewBag.Diseases = model.Diseases;
            //    return View(model);
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home", new { area = "" });
            //}
            //ViewBag.Medications = model.Medications;
            //ViewBag.Allergies = model.ActiveIngredients;
            ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
            //patientID = 1012;
            model = DoctorDbContext.GetAllPatientChronicDisease(patientID, pageNumber, pageSize);
            //DoctorDbContext.GetPatients().Find(Patient => Patient.PatientID == patientID);
            return View(model);
        }
        [HttpGet]
        public IActionResult AddChronicDisease(PatientChronicDiseaseModel model, int id, int pageNumber = 1, int pageSize = 5)
        {
            //int patientID;
            //model.ListPatientChronicDisease.ChronicDisease.PatientID = 1;
            //patientID = model.ListPatientChronicDisease.ChronicDisease.PatientID;
            model.PatientID = id;
            model = DoctorDbContext.GetAllPatientChronicDisease(id, pageNumber, pageSize);
            ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddChronicDisease(PatientChronicDiseaseModel model, int id)
        {
            model.PatientID = id;
            if (ModelState.IsValid)
            {
                bool isAdded = DoctorDbContext.AddPatientChronicDisease(model);
                if (isAdded)
                {
                    TempData["Message"] = "Chronic Disease Added Successfully";
                    return RedirectToAction("AddChronicDisease");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddAcuteDisease(PatientAcuteDiseaseModel model, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
            int patientID = 1012;
            model = DoctorDbContext.GetAllPatientAcuteDisease(patientID,pageNumber, pageSize);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddAcuteDisease(PatientAcuteDiseaseModel model)
        {
            return View();
        }
        [HttpPost]
        public IActionResult PatientDiagnosis(PatientDiagnosisViewModel model,int patientID, [FromForm(Name = "diseaseMethod")] string method)
        {
            //model.ChronicDisease.PatientID = 1012;
            //model.AcuteDisease.PatientID = 1012;
            //model.Allergy.PatientID = 1012;
            //model.AcuteMedication.PatientID = 1012;
            //if (ModelState.IsValid)
            //{
          
            //switch (method)
            //{
            //    //case "chronic": bool chronicAdded = DoctorDbContext.AddPatientChronicDisease(model); TempData["Message"] = "Chronic Disease Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
            //    //case "acute": bool addAcute = DoctorDbContext.AddPatientAcuteDisease(model); TempData["Message"] = "Acute Disease Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
            //    //case "chronicMeds": bool addChroMeds = DoctorDbContext.AddPatientChronicMedication(model); TempData["Message"] = "Chronic Medication Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
            //   // case "acuteMeds": bool addAcuteMeds = DoctorDbContext.AddPatientAcuteMedication(model); TempData["Message"] = "Acute Medication Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
            //    //case "allerges": bool addAllerges = DoctorDbContext.AddPatientAllergies(model); TempData["Message"] = "Allerge Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
                    
            //}

            //if (chronicAdded)
            //{
            //    TempData["Message"] = "Chronic Disease Added Successfully";
            //    return RedirectToAction("PatientDiagnosis");
            //}
            //else if (chronicAdded)
            //{
            //    DoctorDbContext.AddPatientAcuteDisease(model);
            //    TempData["Message"] = "Acute Disease Added Successfully";
            //    return RedirectToAction("PatientDiagnosis");
            //}
            //else if (addChroMeds)
            //{
            //    TempData["Message"] = "Chronic Medication Added Successfully";
            //    return RedirectToAction("PatientDiagnosis");
            //}
            //else if (addAcuteMeds == true)
            //{
            //    TempData["Message"] = "Acute Medication Added Successfully";
            //    return RedirectToAction("PatientDiagnosis");
            //}
            //else if (addAllerges == true)
            //{
            //    TempData["Message"] = "Allerge Added Successfully";
            //    return RedirectToAction("PatientDiagnosis");
            //}
            //else
            //{
            //    return View(model);
            //}



            //}
            return View(model);
        }
        public IActionResult PatientResults(string idNumber)
        {
            int id = UserSingleton.GetLoggedUser().UserId;
            if (UserIsVerified("Doctor"))
            {
                var model = new PatientViewModel(idNumber,id);
                PatientModel.SetPatient(DoctorDbContext.GetPatientWithId(model.Patient.PatientId));
                
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
