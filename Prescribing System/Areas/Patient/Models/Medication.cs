using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
namespace Prescribing_System.Areas.Patient.Models
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(15)]
        public int DosageID { get; set; }
        public int ScheduleID { get; set; }
        protected PatientDbcontext Data = new PatientDbcontext();
        public List<Med_Ingred> GetIngredients()
        {
            return Data.GetAllMedicationIngredient().FindAll(x => x.MedicationID == MedicationID);
        }
    }
}
