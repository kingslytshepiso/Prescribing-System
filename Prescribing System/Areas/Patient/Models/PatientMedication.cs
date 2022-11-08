namespace Prescribing_System.Areas.Patient.Models
{
    public class PatientMedication
    {
        public int ChronicMedID { get; set; }
        public int PatientID { get; set; }
        public int MedIngreID { get; set; }
        public int MedicationID { get; set; }
        public int ActiveIngredientID { get; set; }
        public int DosageID { get; set; }
        public string MedName { get; set; }
        public string Date { get; set; }

        //public Medication Patient { get; set; }
        protected PatientDbcontext Data = new PatientDbcontext();
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
