using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientAcuteMedicationModel
    {
        public DoctorDbContext DbContext { get; set; }
        public MedicationIngredient MedicationIngredient { get; set; }
        public ListPatientAcuteMedication acuteMedication { get; set; }
        public List<ListPatientAcuteMedication> lists = new List<ListPatientAcuteMedication>();
        public int PatientID { get; set; }
        public int OverallCount = 0;
        public int CurrentPage = 1;

        public PatientAcuteMedicationModel()
        {

        }
    }
}
