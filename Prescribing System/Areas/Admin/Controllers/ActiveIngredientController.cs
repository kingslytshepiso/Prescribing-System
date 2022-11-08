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
    public class ActiveIngredientController : Controller
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
        public IActionResult Index(int pageNumber = 1, int pageSize = 5, string sortBy = "none", string keyword = "")
        {
            if (UserIsVerified("Admin"))
            {
                var model = Data.GetAllActIngreWithPaging(pageNumber, pageSize, sortBy);
                if (model.DataList.Count > 0)
                {
                    if (String.IsNullOrEmpty(keyword))
                    {
                        switch (sortBy)
                        {
                            case "none": break;
                            case "name": model.DataList = model.DataList.OrderBy(x => x.Name).ToList(); break;
                        }
                    }
                    else
                    {
                        model.DataList =
                            model.DataList.FindAll(x => x.Name.ToLower().IndexOf(keyword.ToLower()) >= 0 ||
                            x.Description.ToLower().IndexOf(keyword.ToLower()) > 0).ToList();
                    }
                }
                ViewBag.Keyword = keyword;
                return View(model);

            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = Data.GetActIngreWithId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ActiveIngredient model)
        {
            if (ModelState.IsValid)
            {
                var result = Data.UpdateActiveIngredient(model);
                if (result)
                {
                    TempData["Message"] = "Successfully updated active ingredient.";
                    return RedirectToAction("Index", "ActiveIngredient");
                }
            }
            ModelState.AddModelError("", "Invalid value");
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ActiveIngredient model)
        {
            if (ModelState.IsValid) 
            {
                var result = Data.AddActiveIngredient(model);
                if (result)
                {
                    TempData["Message"] = "Successfully added a active ingredient.";
                    return RedirectToAction("Index", "ActiveIngredient");
                }
                ModelState.AddModelError("", "Item already exist");
            }
            ModelState.AddModelError("", "Invalid value");
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (UserIsVerified("Admin"))
            {
                var result = Data.DeleteActiveIngredient(id);
                if (result)
                {
                    TempData["Message"] = "Active Ingredient removed";
                    return RedirectToAction("Index", "ActiveIngredient");
                }
                else
                {
                    TempData["Message"] = "Error removing";
                    return RedirectToAction("Index", "ActiveIngredient");
                }
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
