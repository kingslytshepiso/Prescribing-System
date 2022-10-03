using Microsoft.VisualBasic;
using System;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PatientMedicalHistory
    {
        public int MedHistoryId { get; set; }
        public DateTime DateDiagnosed { get; set; }
        public int PatientID { get; set; }
        public int DiseaseID { get; set; }
    }
}
