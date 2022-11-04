namespace Prescribing_System.Areas.Patient.Models
{
    public class Med_Ingred
    {
        public int MedIngreID { get; set; }
        public int MedicationID { get; set; }
        public int ActiveIngredientID { get; set; }
        public double ActiveStrength { get; set; }
        public string Description { get; set; }
        public int PrescriptionID { get; set; }

    }
}
