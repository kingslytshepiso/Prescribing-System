using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Prescribing_System.Areas.Pharmacist.Controllers
{
    [Area("Pharmacist")]
    [Route("[area]/[controller]s/[action]/{id?}")]

    public class PatientController : Controller
    {
        public PharmacistDbcontext Data = new PharmacistDbcontext();
        public bool UserIsVerified(string role = "")
        {
            var session = new MySession(HttpContext.Session);
            var loggedUserRole = session.GetRole();
            if (loggedUserRole != "none" && loggedUserRole == role)
                return true;
            else
                return false;
        }
        public IActionResult Index(string idNumber)
        {
            if (UserIsVerified("Pharmacist"))
            {
                var model = new PatientViewModel(idNumber);
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult Line(int id)
        {
            if (UserIsVerified("Pharmacist"))
            {
                var model = Data.GetPrescLineWithId(id);
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpPost]
        public IActionResult Dispense(int id, PrescriptionLine model)
        {
            if (UserIsVerified("Pharmacist"))
            {
                var line = Data.GetPrescLineWithId(id);
                if (model.GetValidations().Count <= 0)
                {
                    model = line;
                    line.SetAlerts(ValidateLine(line));
                    var valid = !(line.GetValidations().Any(x => x.Status == "Invalid"));
                    if (valid)
                    {
                        var results = Data.DispenseMedication(line);
                        if (results)
                        {
                            TempData["Message"] = "Successfully dispensed";
                        }
                        else
                            TempData["Message"] = "There was an error while dispensing";
                    }
                    else
                    {
                        TempData["Message"] = "There are warnings regarding this process, view the alerts section below";
                    }
                }
                else
                {
                    line.SetAlerts(Revalidate(model));
                    var valid = !(line.GetValidations().Any(x => x.Status == "Invalid"));
                    if (valid)
                    {
                        var alertsAdded = Data.AddAlerts(line.GetValidations());
                        if (alertsAdded)
                        {
                            var results = Data.DispenseMedication(line);
                            if (results)
                            {
                                TempData["Message"] = "Successfully dispensed";
                            }
                            else
                                TempData["Message"] = "There was an error while dispensing";
                        }
                        else
                        {
                            TempData["Message"] = "There was an error while dispensing";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "There are warnings regarding this process, view the alerts section below";
                    }
                }
                return View("Line", line);
            }
            else
                return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpPost]
        [Route("for-{slug}")]
        public IActionResult RejectLine(int id, string message)
        {
            var line = Data.GetPrescLineWithId(id);
            if (!(string.IsNullOrEmpty(message)))
            {
                line.Status = "Rejected";
                var results = Data.RejectPrescriptionLine(line, message);
                if (results)
                {
                    TempData["Message"] = "Line Rejected";
                    return View("line", line);
                }
                //return Dispense(id, line);
            }
            TempData["Message"] = "Error rejecting the prescription line.";
            return View("Line", line);
        }
        protected List<Alert> Revalidate(PrescriptionLine line)
        {
            var model = new List<Alert>();
            foreach (var v in line.GetValidations())
            {
                if (v.Ignored && !(String.IsNullOrEmpty(v.TempReason)))
                {
                    v.Status = "Ignored";
                    v.StatusReason = v.TempReason;
                }
                model.Add(v);
            }
            return model;
        }
        protected List<Alert> ValidateLine(PrescriptionLine line)
        {
            var model = new List<Alert>();
            if (line.IsStatusValid())
                model.Add(
                    new Alert()
                    {
                        AlertType = "Status",
                        Status = "Valid",
                    });
            else
                model.Add(
                    new Alert()
                     {
                         LineID = line.LineID,
                         AlertType = "Status",
                         Message = "The status of this line does not allow dispensing",
                         Status = "Invalid",
                         StatusReason = "Invalid Status",
                         UserID = line.GetPatient().PatientId,
                         Extras = "You need to have a new prescription prescribed"
                     });
            if (line.IsRepeatValid())
                model.Add(new Alert()
                {
                    AlertType = "Repeat Number",
                    Status = "Valid",
                });
            else
                model.Add(new Alert()
                {
                    LineID = line.LineID,
                    AlertType = "Repeat Number",
                    Message = "Cant dispense on this repeat number",
                    Status = "Invalid",
                    StatusReason = "Invalid - Repeat Number",
                    UserID = line.GetPatient().PatientId,
                    Extras = "Repeats have been depleted, need to have the doctor prescribe more. :)",
                });
            if (line.IsAllergyValid())
                model.Add(new Alert()
                {
                    AlertType = "Allergies",
                    Status = "Valid",
                });
            else
                model.Add(new Alert()
                {
                    LineID = line.LineID,
                    AlertType = "Allergies",
                    Message = "Some of the medication specified may provoke allergies",
                    Status = "Invalid",
                    StatusReason = "Invalid - Allergies",
                    UserID = line.GetPatient().PatientId,
                    Extras = ("The patient is allergic to {0}", line.ListAllergies()).ToString(),
                });
            if (line.IsContraValid())
                model.Add(new Alert()
                {
                    AlertType = "Contra Indications",
                    Status = "Valid",
                });
            else
                model.Add(new Alert()
                {
                    LineID = line.LineID,
                    AlertType = "Contra Indications",
                    Message = "The medication is contra indicated for the patient's condition",
                    Status = "Invalid",
                    StatusReason = "Invalid - Contra Indications",
                    UserID = line.GetPatient().PatientId,
                });
            if (line.IsInteractionValid())
                model.Add(new Alert()
                {
                    AlertType = "Medication Interactions",
                    Status = "Valid",
                });
            else
                model.Add(new Alert()
                {
                    LineID = line.LineID,
                    AlertType = "Medication Interactions",

                    Message = "The Medication specified interacts with one of the medications that the " +
                    "patient is currently on",
                    Status = "Invalid",
                    StatusReason = "Invalid - Medication Interactions",
                    UserID = line.GetPatient().PatientId,
                    Extras = line.ListInteractions()
                });
            if (line.IsDateValid())
            {
                model.Add(new Alert()
                {
                    AlertType = "Last Refill",
                    Status = "Valid",
                });
            }
            else
            {
                model.Add(new Alert()
                {
                    LineID = line.LineID,
                    AlertType = "Last Refill",
                    Message = "20 days has not passed since your last refill",
                    Status = "Valid",
                    StatusReason = "Invalid - Last Refill",
                    UserID = line.GetPatient().PatientId,
                    Extras = "The patient is not allowed to refill before 20 days have passed",
                });
            }
            return model;
        }
    }
}
