using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;

namespace Prescribing_System.Areas.Pharmacist.Controllers
{
    [Area("Pharmacist")]
    [Route("[area]/[controller]Analytics/[action]")]
    public class AnalyticController : Controller
    {
        public PharmacistDbcontext Data = new PharmacistDbcontext();
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult PrescriptionLines()
        {
            var model = Data.GetPrescLinesAnalyticWithPharmacistId(UserSingleton.GetLoggedUser().UserId);
            return View(model);
        }
        public IActionResult Medication()
        {
            var model = new PrescMedicationAnalytic(); 
            return View(model);
        }
    }
}
