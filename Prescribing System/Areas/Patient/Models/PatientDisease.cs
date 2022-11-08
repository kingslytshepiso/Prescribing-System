using System;

namespace Prescribing_System.Areas.Patient.Models
{
    public class PatientDisease
    {
        public int PatientDiseaseID { get; set; }
        public DateTime Date { get; set; }
        public int PatientID { get; set; }
        public int DiseaseID { get; set; }
        protected PatientDbcontext Data = new PatientDbcontext();
        public Disease GetChronicDiseases()
        {
            return Data.GetAllDiseases().Find(x => x.DiseaseID == DiseaseID);
        }
    }
}
