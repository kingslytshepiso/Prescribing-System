using Microsoft.AspNetCore.Mvc;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    public class PrescriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
