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
    [Route("[area]/[controller]/[action]/{id?}")]

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
        public IActionResult Index(string sortBy = "none", string filterBy = "all")
        {
            //Testing
            var urlCheck = this.Url;
            var model = new PatientViewModel(PatientModel.GetPatient().IdNumber);
            if (UserIsVerified("Pharmacist"))
            {
                switch (sortBy)
                {
                    case "none":model.Prescriptions = model.Prescriptions.OrderBy(x => x.Status)
                            .ToList();break;
                    case "datenewest":model.Prescriptions = model.Prescriptions.OrderBy(x => x.Status)
                            .ThenByDescending(x => x.Date).ToList();break;
                    case "dateoldest":model.Prescriptions = model.Prescriptions.OrderBy(x => x.Status)
                            .ThenBy(x => x.Date).ToList();break;
                }
                switch (filterBy)
                {
                    case "all": model.Prescriptions = model.Prescriptions
                            .FindAll(x => x.HasLines()); break;
                    case "active": model.Prescriptions = model.Prescriptions
                            .FindAll(x => x.HasLines() && x.Status == "Active"); break;
                    case "inactive":
                        model.Prescriptions = model.Prescriptions
                        .FindAll(x => x.HasLines() && x.Status == "Inactive"); break;
                    case "rejected": model.Prescriptions = model.Prescriptions
                            .FindAll(x => x.HasLines() && x.Status == "Rejected"); break;
                    default:model.Prescriptions = model.Prescriptions
                            .FindAll(x => x.HasLines() && x.HasLines()); break;
                }
                ViewBag.CurrentSort = sortBy;
                ViewBag.CurrentFIlter = filterBy;
                return View(model);
            }
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
        }
        public IActionResult Line(int id)
        {
            if (UserIsVerified("Pharmacist"))
            {
                var model = Data.GetPrescLineWithId(id);
                return View(model);
            }
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
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
                            line = Data.GetPrescLineWithId(id);
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
                        //Add alerts first
                        var alertsAdded = Data.AddAlerts(line.GetValidations()
                            .FindAll(x => x.Status == "Ignored"));
                        if (alertsAdded)
                        {
                            //Re-dispense the medication again
                            var results = Data.DispenseMedication(line);
                            if (results)
                            {
                                TempData["Message"] = "Successfully dispensed";
                                line = Data.GetPrescLineWithId(id);
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
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url.RouteUrl("") });
        }
        [HttpPost]
        [Route("for-{slug}")]
        public IActionResult RejectLine(int id, string lineMessage)
        {
            if (UserIsVerified("Pharmacist"))
            {
                var line = Data.GetPrescLineWithId(id);
                if (!(string.IsNullOrEmpty(lineMessage)))
                {
                    line.Status = "Rejected";
                    var results = Data.RejectPrescriptionLine(line, lineMessage);
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
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
        }
        public IActionResult RejectPrescription(int id, string prescMessage)
        {
            if (UserIsVerified("Pharmacist"))
            {
                var prescription = Data.GetAllPrescriptions().Find(x => x.PrescriptionID == id);
                var line = Data.GetPrescLineWithId(id);
                if (!(string.IsNullOrEmpty(prescMessage)))
                {
                    prescription.Status = "Rejected";
                    var results = Data.RejectPrescription(prescription, prescMessage);
                    if (results)
                    {
                        TempData["Message"] = "Prescription Rejected";
                        return RedirectToAction("Index", "Patient");
                    }
                    //return Dispense(id, line);
                }
                TempData["Message"] = "Error rejecting the prescription.";
                return View("Line", line);
            }
            else
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = this.Url });
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
                    Status = "Invalid",
                    StatusReason = "Invalid - Last Refill",
                    UserID = line.GetPatient().PatientId,
                    Extras = "The patient is not allowed to refill before 20 days have passed",
                });
            }
            return model;
        }
    }
}
