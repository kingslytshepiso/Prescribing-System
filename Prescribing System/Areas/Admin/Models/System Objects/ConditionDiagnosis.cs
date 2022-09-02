using Prescribing_System.Areas.Admin.Models.System_Users;
using Prescribing_System.Models;
using System;
using System.Collections.Generic;

namespace Prescribing_System.Areas.Admin.Models
{
    public class ConditionDiagnosis
    {
        public int ConditionID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; } 
        public int PatientID { get; set; }
        protected AdminDbContext Data = new AdminDbContext();
        public PatientUser GetPatient()
        {
            return Data.GetPatientWithId(PatientID);
        }
        public List<PatientUser> GetPatients()
        {
            return Data.GetAllPatients();
        }
    }
}
