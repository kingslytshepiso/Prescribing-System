using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class ChronicMedication:Medication
    {
        public int ChronicMedID { get; set; }
        public int PatientID { get; set; }
        public int MedIngreID { get; set; }
        public string MedName { get; set; }

        //public Medication Patient { get; set; }
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
