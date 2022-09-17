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
    public class ContraIndicationController : Controller
    {
        protected AdminDbContext Data = new AdminDbContext();
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string sortBy = "none")
        {
            if (UserIsVerified("Admin"))
            {
                var model = Data.GetAllContraIndicationsWithPaging(pageNumber, pageSize, sortBy);
                switch (sortBy)
                {
                    case "none": break;
                    case "activeingredient": model.DataList = model.DataList.OrderBy(x => x.ActiveIngredientName).ToList(); break;
                    case "disease": model.DataList = model.DataList.OrderBy(x => x.DiseaseName).ToList(); break;
                    case "description": model.DataList = model.DataList.OrderBy(x => x.Description).ToList(); break;
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
            ViewBag.Diseases = Data.GetAllDiseases();
            return View();
        }
        [HttpPost]
        public IActionResult Add(ContraIndication model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.AddContraIndication(model);
                if (result)
                {
                    TempData["Message"] = "Contra indication added.";
                    return RedirectToAction("Index", "ContraIndication");
                }
                ModelState.AddModelError("", "Error adding.");
            }
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            ViewBag.Diseases = Data.GetAllDiseases();
            ModelState.AddModelError("", "Invalid values.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            ViewBag.Diseases = Data.GetAllDiseases();
            var model = Data.GetCntrIndWithId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ContraIndication model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdateContraIndication(model);
                if (result)
                {
                    TempData["Message"] = "Contra indication Updated.";
                    return RedirectToAction("Index", "ContraIndication");
                }
                ModelState.AddModelError("", "Error updating.");
            }
            ViewBag.ActiveIngredients = Data.GetAllActiveIngredients();
            ViewBag.Diseases = Data.GetAllDiseases();
            ModelState.AddModelError("", "Invalid values.");
            return View(model);
        }
    }
}
