using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Patient.Models
{
    public class ListMedViewModel
    {
        public List<Med_Ingred> DataList { get; set; }
        public List<Medication> Meds { get; set; }
        public List<ActiveIngredient> Ingredients { get; set; }
        public List<Dosage> Dosages { get; set; }
        public List<Schedule> Schedules { get; set; }
        public string SearchValue { get; set; }
        public int OverallCount = 0;
        public int CurrentPage = 1;
        protected PatientDbcontext gData = new PatientDbcontext();
    }
}
