using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Prescribing_System.Areas.Doctor.Models;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class AllergyController : Controller
    {
        public DoctorDbContext DbContext = new DoctorDbContext();
        [HttpGet]
        public IActionResult Add(PatientAllergyModel model, int id, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.Allergies = DbContext.GetAllActiveIngredients();
            model.PatientID = id;
            model = DbContext.GetAllPatientAllergy(id, pageNumber, pageSize);
            return View(model);
        }
        public IActionResult Add(PatientAllergyModel model, int id)
        {
            model.PatientID = id;
            if (ModelState.IsValid)
            {
                bool isAdded = DbContext.AddPatientAllergies(model);
                if (isAdded)
                {
                    TempData["Message"] = "Allergy added successfully";
                    return RedirectToAction("Add");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id,Allergy model)
        {
            ViewBag.Allergies = DbContext.GetAllActiveIngredients();
            model = DbContext.GetAllergyById(id);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Allergy model)
        {
            if (ModelState.IsValid)
            {
                bool result = DbContext.UpdatePatientAllergy(model);
                if (result)
                {
                    TempData["Message"] = "Allergy updated successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id,Allergy model)
        {
            model = DbContext.GetAllergyById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool result = DbContext.DeletePatientAllergy(id);
                if (result)
                {
                    TempData["Message"] = "Allergy deleted successfully";
                    return RedirectToAction("Add");
                }
            }
            TempData["Message"] = "Allergy not deleted Successfully";
            return RedirectToAction("Add");
        }
    }
}
