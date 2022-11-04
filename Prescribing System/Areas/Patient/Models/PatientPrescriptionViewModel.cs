using Prescribing_System.Models;
using System.Collections.Generic;
using Prescribing_System.Areas.Admin.Models.System_Users;
using Prescribing_System.Areas.Admin.Models.System_Objects;

namespace Prescribing_System.Areas.Patient.Models
{
   public class PatientPrescriptionViewModel
    {
        public Prescribing_System.Models.Patient UserPatient { get; set; }
        public User UserDetails { get; set; }
        //public Doctor Doctor { get; set; }
        
        public PatientDbcontext dataDoctor = new PatientDbcontext();
        public Prescription Prescription { get; set; }
        public Medication Medication { get; set; }
        public Dosage Dosage { get; set; }
        
        public PrescriptionLine PrescriptionLine { get; set; }

        //public List<PatientUser> Patients { get; set; }
        //public List<Prescription> Prescriptions { get; set; }
        public List<Medication> Medications { get; set; }
        public List<Dosage> Dosages { get; set; }
        public List<PatientPrescriptionViewModel> patients;
        public List<GenericPrescriptionLine> DataList = new List<GenericPrescriptionLine>();
        //public PatientPrescriptionViewModel()
        //{
        //    Medications = dataDoctor.GetAllMeds();
        //    Dosages = dataDoctor.GetAllDosage();
        //}
        //public PatientUser
        public int MedicationID { get; set; }
    }
    public class GenericPrescriptionLine
    {
        public PrescriptionLine Line { get; set; }
        public Prescription Prescription { get; set; }
        public PatientUser Patient { get; set; }
        public DoctorUser Doctor { get; set; }
        public Dosage Dosage { get; set; }
        public Medication Medication { get; set; }
        public MedicalPractice Practice { get; set; }
    }
}
