using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PrescriptionLine
    {
        public int LineID { get; set; }
        public int Quantity { get; set; }
        public string Instruction { get; set; }
        public int RepeatNo { get; set; }
        public int RepeatLeft { get; set; }
        public string Status { get; set; }
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public int DiseaseID { get; set; }
        public string StatusMessage { get; set; }
        public int DosageID { get; set; }
        public int PharmacyID { get; set; }
        public DateTime LastDispensed { get; set; }
        private List<Alert> LineAlerts;
        protected PharmacistDbcontext Data = new PharmacistDbcontext();
        public List<Alert> Alerts
        {
            get
            {
                return LineAlerts;
            }
            set
            {
                LineAlerts = value;
            }
        }
        public List<Alert> GetValidations()
        {
            if (LineAlerts == null)
                return new List<Alert>();
            return LineAlerts;
        }
        public void SetAlerts(List<Alert> alerts)
        {
            LineAlerts = alerts;
        }
        public void AddAlert(Alert alert)
        {
            LineAlerts.Add(alert);
        }
        public Med_Ingred GetMedIngre()
        {
            var med = Data.GetAllMedicationIngredient().Find(x => x.MedicationID == MedicationID);
            if (med != null)
            {
                return med;
            }
            return new Med_Ingred();
        }
        public Medication GetActualMed()
        {
            var med = Data.GetAllMeds().Find(x => x.MedicationID == this.MedicationID);
            if (med != null)
            {
                return med;
            }
            return new Medication();
        }
        public Prescription GetPrescription()
        {
            return Data.GetPrescriptionWithId(PrescriptionID);
        }
        public PatientUser GetPatient()
        {
            return Data.GetPatientWithId(GetPrescription().PatientID);
        }
        public Pharmacy GetPharmacy()
        {
            return Data.GetPharmacyWithId(PharmacyID);
        }
        public List<PatientDisease> GetPatientDiseases()
        {
            return Data.GetAllPatientDiseases().FindAll(x => x.PatientID == GetPatient().PatientId);
        }
        public List<Med_Ingred> GetPatientMedicationIngredients()
        {
            List<Med_Ingred> list = new List<Med_Ingred>();
            var patient_medications = Data.GetAllPatientMedications()
                .FindAll(x => x.PatientID == GetPatient().PatientId);
            foreach (var pm in patient_medications)
            {
                list.Add(Data.GetAllMedicationIngredient()
                    .Find(x => x.MedicationID == pm.MedicationID));
            }
            list.Add(GetMedIngre());
            list.RemoveAll(x => x == null);
            return list;
        }
        public List<Allergy> GetAllergies()
        {
            var Allergies = Data.GetAllergies().FindAll(x => x.PatientID == GetPatient().PatientId);
            if (Allergies == null)
                return new List<Allergy>();
            return Allergies;
        }
        public string ListAllergies()
        {
            string allergies = "";
            foreach (Allergy a in GetAllergies())
            {
                allergies += Data.GetActIngreWithId(a.ActiveIngredientID).Name + "; ";
            }
            return allergies;
        }
        public string ListInteractions()
        {
            string str = "";
            List<MedicationInteraction> interactions = Data.GetAllInteraction()
                .FindAll(x => x.FirstInteractor == this.GetMedIngre().ActiveIngredientID
                || x.ScndInteractor == this.GetMedIngre().ActiveIngredientID);
            foreach (MedicationInteraction i in interactions)
            {
                str += Data.GetActIngreWithId(i.FirstInteractor).Name + " interacts with "
                    + Data.GetActIngreWithId(i.ScndInteractor).Name;
            }
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }
            return str;
        }
        //Validation checks 
        public bool IsRepeatValid()
        {
            if (RepeatLeft > 0)
            {
                return true;
            }
            else return false;
        }
        public bool IsStatusValid()
        {
            return Status == "Prescribed";
        }
        
        public bool IsInteractionValid()
        {
            var patientMeds = GetPatientMedicationIngredients();
            bool foundOne = false;
            var interactions = Data.GetAllInteraction();
            foreach (var pm in patientMeds)
            {
                foreach(var inter in interactions)
                {
                    if (GetActualMed().GetIngredients().Any(x => inter.FirstInteractor == x.ActiveIngredientID)
                   && inter.ScndInteractor == pm.ActiveIngredientID)
                    {
                        foundOne = true;
                        break;
                    }
                    else if (inter.FirstInteractor == pm.ActiveIngredientID
                        && GetActualMed().GetIngredients().Any(x => inter.ScndInteractor == x.ActiveIngredientID))
                    {
                        foundOne=true;
                        break;
                    }
                    else
                        foundOne = false;
                }
                if (foundOne)
                {
                    break;
                }
            }
            if (foundOne)
            {
                return false;
            }
            return true;
        }
        public bool IsContraValid()
        {
            //Checking for contra indications//May need a database change
            var contras = Data.GetAllContraIndications();
            bool valid = true;
            foreach (var m in GetPatientDiseases()) {
                foreach (var c in contras)
                {
                    if (GetActualMed().GetIngredients().Any(x => c.ActiveIngredientID == x.ActiveIngredientID)
                    && c.DiseaseID == m.DiseaseID)
                    {
                        valid = false; break;
                    }
                }
                if (valid)
                {
                    break;
                }
            }
            return valid;
        }
        public bool IsAllergyValid()
        {
            return !GetAllergies().Any(x => x.ActiveIngredientID == GetMedIngre().ActiveIngredientID);
        }
        public bool IsDateValid()
        {
            var minRequiredDate = DateTime.Now.AddDays(-20);
            var pastRequiredDate = LastDispensed > minRequiredDate;
            var valid = pastRequiredDate && !pastRequiredDate;
            return valid;
        }
    }
}
