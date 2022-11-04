using Prescribing_System.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PrescriptionViewModel
    {
        public PatientUseD PatientUseD;
        public Doctor Doctor;
        public Prescription Prescription;
        public List<Prescription> Prescriptions { get; set; }
        public List<PrescriptionLine> Lines { get; set; }

        protected DoctorDbContext DoctorDbContext = new DoctorDbContext();

        public PrescriptionViewModel(int id)
        {
            Doctor = DoctorDbContext.GetDoctorWithId(id);
            Prescriptions = DoctorDbContext.GetPrescriptionsWithDoctorId(id);
        }
        public bool PrescriptionExist()
        {
            if (Prescriptions == null)
                return false;
            else
                return true;
        }
        public PatientUseD GetPatient(int id)
        {
            return DoctorDbContext.GetPatientWithID(id);
        }
        public List<PrescriptionLine> GetPrescriptionLines(int prescId)
        {
            return DoctorDbContext.GetPrescLinesWithPrescId(prescId);
        }
    }
}
