using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Patient.Models;
using Prescribing_System.Areas.Pharmacist.Models;

namespace Prescribing_System.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientController : Controller
    {
        public PatientDbcontext Data = new PatientDbcontext();
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var model = Data.GetAllPatientsWithPaging(pageNumber, pageSize);
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
        //    ViewBag.Diseases = Data.GetAllDiseases();
        //    patientID = 1012;
        //    model = Data.GetAllPatientChronicDisease(patientID, pageNumber, pageSize);
        //    Data.GetPatients().Find(Patient => Patient.PatientID == patientID);
        //    return View(model);
        //}
        public IActionResult Index(int idNumber)
        {
            string myString = idNumber.ToString();
            if (String.IsNullOrEmpty(myString))
            {
                return RedirectToAction("Index", "Home");
            }
            //GETS THE USER THAT'S STORED IN THE STATIC CLASS "UserSingleton"
            var model = new Models.PatientViewModel(idNumber);
            if (UserIsVerified("Pharmacist"))
                return View(model);
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
