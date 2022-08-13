using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PatientViewModel
    {
        public PatientUser Patient { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<PrescriptionLine> Lines { get; set; }
        public DoctorUser Doctor { get; set; }
        protected PharmacistDbcontext DbData = new PharmacistDbcontext();
        public PatientViewModel(string idNumber)
        {
            Patient = DbData.GetPatientWithIdNo(idNumber);
            Prescriptions = DbData.GetPrescriptionsWithId(Patient.PatientId);
        }
        public List<PrescriptionLine> GetPrescriptionLines(int prescId)
        {
            return DbData.GetPrescLinesWithId(prescId);
        }
        public DoctorUser GetDoctor(int id)
        {
            return DbData.GetDoctorWithId(id);
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
