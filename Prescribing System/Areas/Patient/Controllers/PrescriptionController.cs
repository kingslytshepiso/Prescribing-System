using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Patient.Models;
using Microsoft.AspNetCore.Mvc;



namespace Prescribing_System.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PrescriptionController : Controller
    {
        public PatientDbcontext Data = new PatientDbcontext();
        public IActionResult Index(string sortBy = "none")
        {
            var id = UserSingleton.GetLoggedUser().UserId;
            var model = new PatientViewModel(id);
            if (UserIsVerified("Patient"))
            {
                switch (sortBy)
                {
                    case "none": break;
                    case "dateasc": model.Prescriptions = model.Prescriptions
                            .OrderBy(x => x.YearStr)
                            .ThenBy(x => x.MonthStr)
                            .ThenBy(x => x.dayStr)
                            .ToList();break;
                    case "datedesc":
                        model.Prescriptions = model.Prescriptions
                            .OrderByDescending(x => x.YearStr)
                            .ThenByDescending(x => x.MonthStr)
                            .ThenByDescending(x => x.dayStr)
                            .ToList(); break;
                    case "doctor":model.Prescriptions = model.Prescriptions.OrderBy(x => x.DoctorName).ToList();break;
                    case "status": model.Prescriptions = model.Prescriptions.OrderBy(x => x.Status).ToList();break;
                }
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
        public IActionResult Lines(int prescId)
        {
            var model = Data.GetPrescLinesWithPrescId(prescId);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPrescription(int id, int DoctorId, string NowDate, int LineID, PatientPrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                DoctorId = UserSingleton.GetLoggedUser().UserId;
                NowDate = DateTime.Today.ToString();
                bool isAdded = Data.AddPrescription(model, DoctorId, id, NowDate);
                if (isAdded)
                {
                    return RedirectToAction("AddPrescriptionLine");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddPrescriptionLine(PatientPrescriptionViewModel model, int id, int PatientID)
        {
            //var o = new PatientPrescriptionViewModel();
            ViewBag.Medications = model.Medications;
            ViewBag.DosageForms = model.Dosages;
            PatientID = UserSingleton.GetLoggedUser().UserId;
            //id = 1012;
            model = Data.GetAllPresciptionLines(PatientID);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPrescriptionLine(PatientPrescriptionViewModel model, string quatity)
        {
            model.PrescriptionLine.RepeatLeftNo = model.PrescriptionLine.RepeatNo;
            if (ModelState.IsValid)
            {
                bool isAdded = Data.AddPrescriptionLine(   model);
                if (isAdded)
                {
                    return RedirectToAction("AddPrescriptionLine");
                }
            }
            return View(model);
        }
        //public bool UserIsVerified(string role = "")
        //{
        //    var session = new MySession(HttpContext.Session);
        //    var loggedUserRole = session.GetRole();
        //    if (loggedUserRole != "none" && loggedUserRole == role)
        //        return true;
        //    else
        //        return false;
        //}
        //[HttpGet]
        //public IActionResult PatientDiagnosis(int patientID, PatientChronicDiseaseModel model, int pageNumber = 1, int pageSize = 10)
        //{
        //    // if (UserIsVerified("Doctor"))
        //    //{
        //    //    ViewBag.Diseases = model.Diseases;
        //    //    return View(model);
        //    //}
        //    //else
        //    //{
        //    //    return RedirectToAction("Index", "Home", new { area = "" });
        //    //}
        //    //ViewBag.Medications = model.Medications;
        //    //ViewBag.Allergies = model.ActiveIngredients;
        //    ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
        //    patientID = 1012;
        //    model = DoctorDbContext.GetAllPatientChronicDisease(patientID, pageNumber, pageSize);
        //    DoctorDbContext.GetPatients().Find(Patient => Patient.PatientID == patientID);
        //    return View(model);
        //}
        //[HttpGet]
        //public IActionResult AddChronicDisease(int patientID, PatientChronicDiseaseModel model, int pageNumber = 1, int pageSize = 5)
        //{
        //    patientID = 1012;
        //    model = Data.GetAllPatientChronicDisease(patientID, pageNumber, pageSize);
        //    ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
        //    return View(model);
        //}
        //[HttpPost]
        //public IActionResult AddChronicDisease(PatientDiagnosisViewModel model)
        //{
        //    return View();
        //}
        //[HttpGet]
        ////public IActionResult AddAcuteDisease(PatientAcuteDiseaseModel model, int pageNumber = 1, int pageSize = 5)
        //{
        //    ViewBag.Diseases = DoctorDbContext.GetAllDiseases();
        //    int patientID = 1012;
        //    model = DoctorDbContext.GetAllPatientAcuteDisease(patientID, pageNumber, pageSize);
        //    return View(model);
        //}
        //[HttpPost]
        //public IActionResult AddAcuteDisease(PatientAcuteDiseaseModel model)
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult PatientDiagnosis(PatientDiagnosisViewModel model, int patientID, [FromForm(Name = "diseaseMethod")] string method)
        //{
        //    model.ChronicDisease.PatientID = 1012;
        //    model.AcuteDisease.PatientID = 1012;
        //    //model.Allergy.PatientID = 1012;
        //    //model.AcuteMedication.PatientID = 1012;
        //    //if (ModelState.IsValid)
        //    //{

        //    switch (method)
        //    {
        //        case "chronic": bool chronicAdded = DoctorDbContext.AddPatientChronicDisease(model); TempData["Message"] = "Chronic Disease Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
        //        case "acute": bool addAcute = DoctorDbContext.AddPatientAcuteDisease(model); TempData["Message"] = "Acute Disease Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
        //        case "chronicMeds": bool addChroMeds = DoctorDbContext.AddPatientChronicMedication(model); TempData["Message"] = "Chronic Medication Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
        //        case "acuteMeds": bool addAcuteMeds = DoctorDbContext.AddPatientAcuteMedication(model); TempData["Message"] = "Acute Medication Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;
        //        case "allerges": bool addAllerges = DoctorDbContext.AddPatientAllergies(model); TempData["Message"] = "Allerge Added Successfully"; return RedirectToAction("PatientDiagnosis"); break;

        //    }

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
            //return View(model);
        
    }
}


