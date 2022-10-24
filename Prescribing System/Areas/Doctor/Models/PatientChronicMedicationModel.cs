using System.Collections.Generic;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientChronicMedicationModel
    {
        public DoctorDbContext Context { get; set; }
        public MedicationIngredient MedicationIngredient { get; set; }
        public ListPatientChronicMedication List { get; set; }
        public List<ListPatientChronicMedication> chronicMedications = new List<ListPatientChronicMedication>();
        public List<Medication> Medications { get; set; }
        public int PatientID { get; set; }
        public int OverallCount = 0;
        public int CurrentPage = 1;
        public PatientChronicMedicationModel()
        {
            //Medications = Context.GetAllMeds();
        }
    }
}
