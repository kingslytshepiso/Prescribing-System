using System.Collections.Generic;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientListContex
    {
        public List<Prescribing_System.Models.Patient> PatientList;
        public int OverallCount = 0;
        public int CurrentPage = 1;
    }
}
