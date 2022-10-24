using Microsoft.AspNetCore.SignalR;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class MedicationIngredient
    {
        public int MedIngreID { get; set; }
        public int MedicationID { get; set; }
        public int ActiveIngredientID { get; set; }
        public double ActiveStrength { get; set; }
        //from different tables
        public string MedicationName { get; set; }
        public string IngredientName { get; set; }
        protected DoctorDbContext data = new DoctorDbContext();
        public Medication GetMedication()
        {
            return data.GetAllMeds().Find(x => x.MedicationID == MedicationID);
        }
        public ActiveIngredient GetActiveIngredient()
        {
            return data.GetAllActiveIngredients().Find(x => x.ActiveIngreID == ActiveIngredientID);
        }
    }
}
