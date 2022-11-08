using Microsoft.AspNetCore.Mvc;
using Prescribing_System.Areas.Doctor.Models;
using Prescribing_System.Models;
using System.Collections.Generic;
using System;
using System.Linq;

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
        [HttpGet]
        public IActionResult Edit(int id, PrescriptionLine model)
        {
            model = DoctorDbContext.GetPrescLineByLineId(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PrescriptionLine model, int id)
        {
            var line = model;
            model.PresciptionLineID = id;
            if (UserIsVerified("Doctor"))
            {
                if (model.GetValidations().Count <= 0)
                {

                    var valid = !(line.GetValidations().Any(x => x.Status == "Invalid"));
                    if (valid)
                    {
                        bool result = DoctorDbContext.UpdatePrescriptionLine(model);
                        if (result)
                        {
                            TempData["Message"] = "Line updated successfully";
                            return RedirectToAction("Index", "Prescription");
                        }
                        return View(model);
                    }
                     
                }
                else
                {
                    line.SetAlerts(Revalidate(line));
                    var valid = !(line.GetValidations().Any(x => x.Status == "Invalid"));
                    if (valid)
                    {
                        int PrescID = DoctorDbContext.GetPrescriptionLineTop().PresciptionLineID;
                        //PrescID = line.Alert.LineID;
                        var alertsAdded = DoctorDbContext.AddAlerts(line.GetValidations(), PrescID);
                        if (alertsAdded)
                        {
                            model.RepeatLeftNo = model.RepeatNo;
                            bool result = DoctorDbContext.UpdatePrescriptionLine(model);
                            if (result)
                            {
                                TempData["Message"] = "Line updated successfully";
                                return RedirectToAction("Index", "Prescription");
                            }
                            else
                            {
                                TempData["Message"] = "Line Not updated Successfully";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "There are warnings regarding this process, view the alerts section below";
                        }
                    }

                }

                return View(model);

            }
            return View();
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
           
            if (line.IsAllergyValid())
                model.Add(new Alert()
                {
                    AlertType = "Allergies",
                    Status = "Valid",
                });
            else
                model.Add(new Alert()
                {
                    LineID = line.PresciptionLineID,
                    AlertType = "Allergies",
                    Message = "Some of the medication specified may provoke allergies",
                    Status = "Invalid",
                    StatusReason = "Invalid - Allergies",
                    UserID = line.GetPatient().PatientID,
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
                    LineID = line.PresciptionLineID,
                    AlertType = "Contra Indications",
                    Message = "The medication is contra indicated for the patient's condition",
                    Status = "Invalid",
                    StatusReason = "Invalid - Contra Indications",
                    UserID = line.GetPatient().PatientID,
                });
            
            return model;
        }
    }
}
