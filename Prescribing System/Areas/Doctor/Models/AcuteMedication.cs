using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class AcuteMedication:Medication
    {
        public int AcuteMedicationID { get; set; }
        public int PatientID { get; set; }
        //public int MedicationID { get; set; }
        //protected DoctorDbContext Data = new DoctorDbContext();
        public Medication GetMedications()
        {
            return Data.GetAllMeds().Find(x => x.MedicationID == MedicationID);
        }
        public ActiveIngredient GetActiveIngredients()
        {
            return Data.GetAllActiveIngredients().Find(x => x.ActiveIngreID == ActiveIngredientID);
        }
    }
}
