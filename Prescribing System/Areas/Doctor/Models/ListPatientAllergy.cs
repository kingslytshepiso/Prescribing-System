using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class ListPatientAllergy
    {
        public Prescribing_System.Models.Patient Patient { get; set; }
        public ActiveIngredient ActiveIngredient { get; set; }
        public Allergy Allergy { get; set; }
    }
}
