using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientAllergyModel
    {
        public DoctorDbContext Context { get; set; }
        public ListPatientAllergy allergy { get; set; }
        public List<ListPatientAllergy> lists = new List<ListPatientAllergy>();
        public int PatientID { get; set; }
        public int OverallCount = 0;
        public int CurrentPage = 1;
        public PatientAllergyModel()
        {

        }
    }
}
