using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Doctor.Models;


namespace Prescribing_System.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    public class PrescriptionController : Controller
    {
        public DoctorDbContext DoctorDbContext = new DoctorDbContext();
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var model = DoctorDbContext.GetAllPatientsWithPaging(pageNumber, pageSize);
            if (UserIsVerified("Doctor"))
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

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
        [HttpGet]
        public IActionResult Add(int id)
        {
            if (UserIsVerified("Doctor"))
            {
                var model = DoctorDbContext.GetPatientWithId(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        [HttpPost]
        public IActionResult Add(int id, int DoctorId, string NowDate, PatientPrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                DoctorId = UserSingleton.GetLoggedUser().UserId;
                NowDate = DateTime.Today.ToString();
                bool isAdded = DoctorDbContext.AddPrescription( DoctorId, id, NowDate);
                if (isAdded)
                {
                    return RedirectToAction("AddPrescriptionLine");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddPrescription(int id)
        {
            if (UserIsVerified("Doctor"))
            {
                var model = DoctorDbContext.GetPatientWithId(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
        [HttpPost]
        public IActionResult AddPrescription(int id,int DoctorId,string NowDate,PrescriptionLine model)
        {
            if (ModelState.IsValid)
            {
                DoctorId = UserSingleton.GetLoggedUser().UserId;
                NowDate = DateTime.Today.ToString();
                bool isAdded = DoctorDbContext.AddPrescription(DoctorId,id,NowDate);
                if (isAdded)
                {
                    
                    return RedirectToAction("AddPrescriptionLine");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddPrescriptionLine(AddPrescriptionLineViewModel line)
        {
            int id = DoctorDbContext.GetPrescriptionTop().PrescriptionID;
            var model = new AddPrescriptionLineViewModel()
            {
                DataList = DoctorDbContext
                .GetPrescLinesByDoctorIdPatienId(id),
                line = new PrescriptionLine(),

            };
            //PrescriptionID = PrescriptionModel.GetPrescription().PrescriptionID;
            ViewBag.Medications = DoctorDbContext.GetAllMeds();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPrescriptionLine(AddPrescriptionLineViewModel model,int id)
        {
            var line = DoctorDbContext.GetPrescLineWithId(id);
            if (model.line.GetValidations().Count <= 0)
            {
                if (ModelState.IsValid)
                {
                    var lines = new PrescriptionLine();
                    lines = model.line;
                    lines.SetAlerts(ValidateLine(lines));
                    
                    var valid = !(lines.GetValidations().Any(x => x.Status == "Invalid"));
                    if (valid)
                    {
                        model.line.RepeatLeftNo = model.line.RepeatNo;
                        bool isAdded = DoctorDbContext.AddPrescriptionLine(model);
                        if (isAdded)
                        {
                            TempData["Message"] = "Line Added Successfully";
                            return RedirectToAction("AddPrescriptionLine");
                        }
                        else
                        {
                            TempData["Message"] = "Line Not Added Successfully";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "There are warnings regarding this process, view the alerts section below";
                    }    
                }
                else
                {
                    line.SetAlerts(Revalidate(model.line));
                    var valid = !(line.GetValidations().Any(x => x.Status == "Invalid"));
                    if (valid)
                    {
                        var alertsAdded = DoctorDbContext.AddAlerts(line.GetValidations());
                        if (alertsAdded)
                        {
                            model.line.RepeatLeftNo = model.line.RepeatNo;
                            bool isAdded = DoctorDbContext.AddPrescriptionLine(model);
                            if (isAdded)
                            {
                                TempData["Message"] = "Line Added Successfully";
                                return RedirectToAction("AddPrescriptionLine");
                            }
                            else
                            {
                                TempData["Message"] = "Line Not Added Successfully";
                            }
                        }
                        else
                        {
                            TempData["Message"] = "There are warnings regarding this process, view the alerts section below";
                        }
                    }
                }
            }
            
            return View(model);
        }
        protected List<Alert> Revalidate(PrescriptionLine model)
        {
            var models = new List<Alert>();
            foreach (var v in model.GetValidations())
            {
                if (v.Ignored && !(String.IsNullOrEmpty(v.TempReason)))
                {
                    v.Status = "Ignored";
                    v.StatusReason = v.TempReason;
                }
                models.Add(v);
            }
            return models;
        }
        protected List<Alert> ValidateLine(PrescriptionLine model)
        {
            var models = new List<Alert>();
            if (model.IsStatusValid())
                models.Add(
                    new Alert()
                    {
                        AlertType = "Status",
                        Status = "Valid",
                    });
            else
                models.Add(
                    new Alert()
                    {
                        LineID = model.PresciptionLineID,
                        AlertType = "Status",
                        Message = "The status of this line does not allow dispensing",
                        Status = "Invalid",
                        StatusReason = "Invalid Status",
                        UserID = model.GetPatient().PatientID,
                        Extras = "You need to have a new prescription prescribed"
                    });
            if (model.IsRepeatValid())
                models.Add(new Alert()
                {
                    AlertType = "Repeat Number",
                    Status = "Valid",
                });
            else
                models.Add(new Alert()
                {
                    LineID = model.PresciptionLineID,
                    AlertType = "Repeat Number",
                    Message = "Cant dispense on this repeat number",
                    Status = "Invalid",
                    StatusReason = "Invalid - Repeat Number",
                    UserID = model.GetPatient().PatientID,
                    Extras = "Repeats have been depleted, need to have the doctor prescribe more. :)",
                });
            if (model.IsAllergyValid())
                models.Add(new Alert()
                {
                    AlertType = "Allergies",
                    Status = "Valid",
                });
            else
                models.Add(new Alert()
                {
                    LineID = model.PresciptionLineID,
                    AlertType = "Allergies",
                    Message = "Some of the medication specified may provoke allergies",
                    Status = "Invalid",
                    StatusReason = "Invalid - Allergies",
                    UserID = model.GetPatient().PatientID,
                    Extras = ("The patient is allergic to {0}", model.ListAllergies()).ToString(),
                });
            if (model.IsContraValid())
                models.Add(new Alert()
                {
                    AlertType = "Contra Indications",
                    Status = "Valid",
                });
            else
                models.Add(new Alert()
                {
                    LineID = model.PresciptionLineID,
                    AlertType = "Contra Indications",
                    Message = "The medication is contra indicated for the patient's condition",
                    Status = "Invalid",
                    StatusReason = "Invalid - Contra Indications",
                    UserID = model.GetPatient().PatientID,
                });
            if (model.IsInteractionValid())
                models.Add(new Alert()
                {
                    AlertType = "Medication Interactions",
                    Status = "Valid",
                });
            else
                models.Add(new Alert()
                {
                    LineID = model.PresciptionLineID,
                    AlertType = "Medication Interactions",

                    Message = "The Medication specified interacts with one of the medications that the " +
                    "patient is currently on",
                    Status = "Invalid",
                    StatusReason = "Invalid - Medication Interactions",
                    UserID = model.GetPatient().PatientID,
                    Extras = model.ListInteractions()
                });
            if (model.IsDateValid())
            {
                models.Add(new Alert()
                {
                    AlertType = "Last Refill",
                    Status = "Valid",
                });
            }
            else
            {
                models.Add(new Alert()
                {
                    LineID = model.PresciptionLineID,
                    AlertType = "Last Refill",
                    Message = "20 days has not passed since your last refill",
                    Status = "Valid",
                    StatusReason = "Invalid - Last Refill",
                    UserID = model.GetPatient().PatientID,
                    Extras = "The patient is not allowed to refill before 20 days have passed",
                });
            }
            return models;
        }
    }
}
