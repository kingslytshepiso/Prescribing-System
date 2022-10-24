namespace Prescribing_System.Areas.Doctor.Models
{
    public class Medication_ActiveIngredient
    {
        public int MedIngreID { get; set; }
        public int MedicationID { get; set; }
        public int ActiveIngreID { get; set; }
        public string ActiveStrength { get; set; }
        
    }
}
