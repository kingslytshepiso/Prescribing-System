using System.Collections.Generic;

namespace Prescribing_System.Areas.Patient.Models
{
    public class PatientViewModel
    {
        public PatientUser Patient { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<PrescriptionLine> Lines { get; set; }
        public string SortBy = "none";
        public string Order = "ascending";
        public DoctorUser Doctor { get; set; }
        protected PatientDbcontext DbData = new PatientDbcontext();
        protected PatientDbcontext data = new PatientDbcontext();
        public PatientViewModel(int id)
        {
            Patient = data.GetPatientWithId(id);
            Prescriptions = data.GetPrescriptionsWithPatientId(Patient.PatientId);
        }
        public List<PrescriptionLine> GetPrescriptionLines(int prescId)
        {
            return data.GetPrescLinesWithPrescId(prescId);
        }
        public DoctorUser GetDoctor(int id)
        {
            return data.GetDoctorWithId(id);
        }
        //Unfinished method to get the doctor's medical practice
        //allowing the view to access the image related to the practice
        public object GetPractice(int id)
        {
            return new object();
        }
        public bool PatientExist()
        {
            if (Patient == null)
                return false;
            else
                return true;
        }
    }
}
