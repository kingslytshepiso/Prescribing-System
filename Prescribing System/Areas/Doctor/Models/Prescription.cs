using System;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public string PrescriptionDate { get; set; }
        public DateTime Date { get; set; }    
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string PrescrStatus { get; set; }
    }
}
