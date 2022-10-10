using System.Collections.Generic;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientChronicDiseaseModel
    {
        public ListPatientChronicDisease ListPatientChronicDisease { get; set; }
        public ChronicDisease ChronicDisease { get; set; }
        public int PatientID  { get; set; }
        public List<ListPatientChronicDisease> chronicDiseaseModels = new List<ListPatientChronicDisease>();
        public int OverallCount = 0;
        public int CurrentPage = 1;
        public PatientChronicDiseaseModel()
        {

        }
    }
}
