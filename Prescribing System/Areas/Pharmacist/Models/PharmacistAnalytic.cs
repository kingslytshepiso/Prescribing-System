using System;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PharmacistAnalytic
    {
        public int HistoryID { get; set; }
        public int PharmacistID { get; set; }
        public int PatientID { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }
        protected PharmacistDbcontext data = new PharmacistDbcontext();
        public PatientUser GetPatient()
        {
            return data.GetPatientWithId(PatientID);    
        }
    }
}
