namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class Allergy
    {
        public int AllergyID { get; set; }
        public int ActiveIngredientID { get; set; }
        public int PatientID { get; set; }
    }
}
