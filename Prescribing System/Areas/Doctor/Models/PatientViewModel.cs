using Prescribing_System.Areas.Pharmacist.Models;
using System.Collections.Generic;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientViewModel
    {
        public PatientUser Patient { get; set; }
        public PrescriptionLine Line { get; set; }
        public Disease Disease { get; set; }
        public ChronicDisease ChronicDisease { get; set; }
        public List<ChronicDisease> ChronicDiseases { get; set; }
        public List<AcuteDisease> AcuteDiseases { get; set; }
        public List<ChronicMedication> ChronicMedications { get; set; }
        public List<AcuteMedication> AcuteMedications { get; set; }
        public List<Allergy> Allergies { get; set; }
        public List<CurrentDoctorVisit> DoctorVisits { get; set; }
        public List<PatientMedicationModel> PatientMedicationModels { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<PrescriptionLine> Lines { get; set; }
        public Doctor Doctor { get; set; }
        protected DoctorDbContext DbData = new DoctorDbContext();
        public PatientViewModel(string idNumber, int id)
        {
             //int id = 0;
            Patient = DbData.GetPatientWithIdNo(idNumber);
            Prescriptions = DbData.GetPrescriptionsWithPatientId(Patient.PatientId);
            Doctor = DbData.GetDoctorWithId(id);
            ChronicDiseases = DbData.GetChronicDiseasesByPatientId(Patient.PatientId);
            AcuteDiseases = DbData.GetAcuteDiseasesByPatientId(Patient.PatientId);
            Allergies = DbData.GetAllergiesByPatientId(Patient.PatientId);
            ChronicMedications = DbData.GetChronicMedicationsByPatientId(Patient.PatientId);
            AcuteMedications = DbData.GetAcuteMedicationsByPatientId(Patient.PatientId);
            //DoctorVisits = DbData.GetCurrentDoctorVisitsByPatientId(Patient.PatientId);
        }
        public class PatientMedicationModel
        {
            AcuteMedication AcuteMedication { get; set; }
            ChronicMedication ChronicMedication { get; set; }
        }
        public List<PrescriptionLine> GetPrescriptionLines(int prescId)
        {
            return DbData.GetPrescLinesWithPrescId(prescId);
        }
        public Doctor GetDoctor(int id)
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
        public Medication GetMed()
        {
            return DbData.GetAllMeds().Find(x => x.MedicationID == Line.MedicationID);
        }
        
    }
}
