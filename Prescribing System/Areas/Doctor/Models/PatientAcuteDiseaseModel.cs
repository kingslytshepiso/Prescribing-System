using System.Collections.Generic;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientAcuteDiseaseModel
    {
        public DoctorDbContext Context { get; set; }
        public AcuteDisease AcuteDisease { get; set; }
        public ListPatientAcuteDisease acuteDisease { get; set; }
        public List<ListPatientAcuteDisease> lists = new List<ListPatientAcuteDisease>();
        public int PatientID { get; set; }
        public int OverallCount = 0;
        public int CurrentPage = 1;

        public PatientAcuteDiseaseModel()
        {

        }
    }
}
