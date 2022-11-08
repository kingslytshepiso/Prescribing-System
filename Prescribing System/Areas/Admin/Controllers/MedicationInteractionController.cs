using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models;
using Prescribing_System.Areas.Admin.Models.System_Objects;

namespace Prescribing_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicationInteractionController : Controller
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
                var model = Data.GetAllInteractionsWithPaging(pageNumber, pageSize, sortBy);
                switch (sortBy)
                {
                    case "none": break;
                }
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            return View();
        }
        [HttpPost]
        public IActionResult Add(MedicationInteraction model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.AddInteraction(model);
                if (result)
                {
                    TempData["Message"] = "Medication Interaction added.";
                    return RedirectToAction("Index", "MedicationInteraction");
                }
                ModelState.AddModelError("", "Error adding.");
            }
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            ModelState.AddModelError("", "Invalid values.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            var model = Data.GetInteractionWithId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(MedicationInteraction model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdateInteraction(model);
                if (result)
                {
                    TempData["Message"] = "Interaction modified successfully.";
                    return RedirectToAction("Index", "MedicationInteraction");
                }
                ModelState.AddModelError("", "Error updating");
            }
            ModelState.AddModelError("", "Invalid values");
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (UserIsVerified("Admin"))
            {
                var result = Data.DeleteInteraction(id);
                if (result)
                {
                    TempData["Message"] = "Medication interaction removed";
                    return RedirectToAction("Index", "MedicationInteraction");
                }
                else
                {
                    TempData["Message"] = "Error removing";
                    return RedirectToAction("Index", "MedicationInteraction");
                }
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult Search(string keyword)
        {
            List<SearchIndexModel> model = new List<SearchIndexModel>();
            return View("/Home/Search", model);
        }
    }
}
