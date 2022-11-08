using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Objects;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicationController : Controller
    {
        public AdminDbContext Data = new AdminDbContext();
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 5, string sortBy = "none")
        {
            if (UserIsVerified("Admin"))
            {
                var model = Data.GetAllMedicationWithPaging(pageNumber, pageSize, sortBy);
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            ViewBag.Dosages = Data.GetAllDosages();
            ViewBag.Schedules = Data.GetAllSchedules();
            ViewBag.Types = Data.GetAllMedTypes();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Medication model, 
            [FromForm(Name = "ingredientIds")] int[] ingredientIds,
            [FromForm(Name = "SelectedListValues")] int[] strengths)
        {
            if (ModelState.IsValid)
            {
                if (ingredientIds != null || strengths != null
                    && ingredientIds.Length == strengths.Length)
                {
                    model.Ingredients = new List<Medication_Ingredient>();
                    for (int i = 0; i < ingredientIds.Length; i++)
                    {
                        model.Ingredients.Add(new Medication_Ingredient()
                        {
                            ActiveIngredientID = ingredientIds[i],
                            ActiveStrength = strengths[i]
                        });
                    }
                    var result = Data.AddMedication(model);
                    if (result)
                    {
                        TempData["Message"] = "Medication Added";
                        return RedirectToAction("Index", "Medication");
                    }
                }
                else
                    ModelState.AddModelError("", "Error Adding");
            }
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            ViewBag.Dosages = Data.GetAllDosages();
            ViewBag.Schedules = Data.GetAllSchedules();
            ViewBag.Types = Data.GetAllMedTypes();
            ModelState.AddModelError("", "Invalid values.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients().OrderBy(x => x.Name).ToList();
            ViewBag.Dosages = Data.GetAllDosages();
            ViewBag.Schedules = Data.GetAllSchedules();
            ViewBag.Types = Data.GetAllMedTypes();
            var model = Data.GetMedicationAndIngredientsWithId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Medication model, 
            [FromForm(Name = "ingredientIds")] int[] ingredientIds,
            [FromForm(Name = "SelectedListValues")] int[] strengths)
        {
            if (ModelState.IsValid)
            {
                var currentModel = Data.GetMedicationAndIngredientsWithId(model.MedicationID);
                if (ingredientIds != null || strengths != null
                    && ingredientIds.Length == strengths.Length)
                {
                    model.Ingredients = new List<Medication_Ingredient>();
                    for (int i = 0; i < ingredientIds.Length; i++)
                    {
                        if (currentModel.GetIngredients().Any(x => x.ActiveIngredientID == ingredientIds[i]))
                        {
                            model.Ingredients.Add(new Medication_Ingredient()
                            {
                                ActiveIngredientID = ingredientIds[i],
                                ActiveStrength = strengths[i],
                                State = "Old",
                            });
                        }
                        else
                        {
                            model.Ingredients.Add(new Medication_Ingredient()
                            {
                                ActiveIngredientID = ingredientIds[i],
                                ActiveStrength = strengths[i]
                            });
                        }
                    }
                    var result = Data.UpdateMedication(model);
                    if (result)
                    {
                        TempData["Message"] = "Medication updated";
                        return RedirectToAction("Index", "Medication");
                    }
                }
                ModelState.AddModelError("", "Error updated");
            }
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients().OrderBy(x => x.Name).ToList();
            ViewBag.Dosages = Data.GetAllDosages();
            ViewBag.Schedules = Data.GetAllSchedules();
            ViewBag.Types = Data.GetAllMedTypes();
            ModelState.AddModelError("", "Invalid value");
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (UserIsVerified("Admin"))
            {
                var result = Data.DeleteMedication(id);
                if (result)
                {
                    TempData["Message"] = "Medication removed";
                    return RedirectToAction("Index", "Medication");
                }
                else
                {
                    TempData["Message"] = "Error removing";
                    return RedirectToAction("Index", "Medication");
                }
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
