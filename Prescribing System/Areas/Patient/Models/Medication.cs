using System.Collections.Generic;


namespace Prescribing_System.Areas.Patient.Models
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public string Name { get; set; }
        public int DosageID { get; set; }
        public int ScheduleID { get; set; }
        protected PatientDbcontext Data = new PatientDbcontext();
        public List<Med_Ingred> GetIngredients()
        {
            return Data.GetAllMedicationIngredient().FindAll(x => x.MedicationID == MedicationID);
        }
    }
}
