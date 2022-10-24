using Prescribing_System.Areas.Pharmacist.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public string Name { get; set; }
        public int DosageID { get; set; }
        public int ScheduleID { get; set; }
        public int ActiveIngredientID { get; set; }
        protected DoctorDbContext Data = new DoctorDbContext();
        public ActiveIngredient GetIngredient()
        {
            return Data.GetActIngreWithId(ActiveIngredientID);
        }
        public Schedule GetSchedules()
        {
            return Data.GetScheduleById(ScheduleID);
        } 
        public Dosage GetDosage()
        {
            return Data.GetDosageById(DosageID);
        }
        public Medication_ActiveIngredient GetMedicationS()
        {
            return Data.GetMedication_ActiveIngredientById(MedicationID);
        }
        public List<Med_Ingred> GetIngredients()
        {
            return Data.GetAllMedicationIngredient().FindAll(x => x.MedicationID == MedicationID);
        }

    }
}
