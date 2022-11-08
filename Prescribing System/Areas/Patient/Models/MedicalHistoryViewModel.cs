using System.Collections.Generic;

namespace Prescribing_System.Areas.Patient.Models
{
    public class MedicalHistoryViewModel
    {
        public PatientUser PatientUser { get; set; }
        public Disease Disease { get; set; }
        public Medication Medication { get; set; }
        public PatientDisease PatientDisease { get; set; }
        public PatientMedication PatientMedication { get; set; }
        public List<PatientMedication> PatientMedications { get; set; }
        public List<Medication> MedicationList { get; set; }
        public List<PatientDisease> PatientDiseases { get; set; }
        protected PatientDbcontext data = new PatientDbcontext();
        public MedicalHistoryViewModel(int ID)
        {
            PatientDiseases = data.GetPatientDiseasesByPatientId(ID);
            PatientMedications = data.GetChronicMedicationsByPatientId(ID);
        }
    }
}
