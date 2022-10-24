using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PatientViewModel
    {
        public PatientUser Patient;
        
        public List<Prescription> Prescriptions { get; set; }
        public List<PrescriptionLine> Lines { get; set; }
        public DoctorUser Doctor { get; set; }
        protected PharmacistDbcontext DbData = new PharmacistDbcontext();
        public PatientViewModel(string idNumber)
        {
            Patient = DbData.GetPatientWithIdNo(idNumber);
            PatientModel.SetPatient(Patient);
            Prescriptions = DbData.GetPrescriptionsWithPatientId(Patient.PatientId);
        }
        public List<PrescriptionLine> GetPrescriptionLines(int prescId)
        {
            return DbData.GetPrescLinesWithPrescId(prescId);
        }
        public DoctorUser GetDoctor(int id)
        {
            return DbData.GetDoctorWithId(id);
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
