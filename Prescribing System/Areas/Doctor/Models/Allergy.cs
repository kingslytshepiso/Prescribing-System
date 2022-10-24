using Prescribing_System.Areas.Pharmacist.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class Allergy
    {
        public int AllergyID { get; set; }
        public int PatientID { get; set; }
        public int ActiveIngreID { get; set; }
        protected DoctorDbContext Data = new DoctorDbContext();
        public ActiveIngredient GetIngredient()
        {
            return Data.GetActIngreWithId(ActiveIngreID);
        }
    }
}
