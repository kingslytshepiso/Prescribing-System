using Microsoft.AspNetCore.Mvc;
using Prescribing_System.Areas.Doctor.Models;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class PrescriptionLineController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        public IActionResult Index(AddPrescriptionLineViewModel model,int id)
        {
            if (UserIsVerified("Doctor"))
            {
                model= DoctorDbContext.GetAddPrescriptionLines(id);
                //PrescriptionModel.SetPrescription(DoctorDbContext.GetPrescriptionWithId(model.line.PrescriptionID));
                return View(model);
            }
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
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
        public IActionResult Line(int id)
        {
            if (UserIsVerified("Doctor"))
            {
                var model = DoctorDbContext.GetPrescLineWithId(id);
                return View(model);
            }
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
        }
        public IActionResult Delete(int id)
        {
            if (UserIsVerified("Doctor"))
            {
                if (ModelState.IsValid)
                {
                    bool result = DoctorDbContext.DeletePrescriptionLine(id);
                    if (result)
                    {
                        TempData["Message"] = "Item deleted successfully";
                        return RedirectToAction("Index");
                    }
                    TempData["Message"] = "Acute disease not deleted successfully";
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
        }
    }
}
