using System;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PatientMedication
    {
        public int PatientMedID { get; set; }
        public int PatientID { get; set; }
        public int MedicationID { get; set; }
        public DateTime Date { get; set; }
    }
}
