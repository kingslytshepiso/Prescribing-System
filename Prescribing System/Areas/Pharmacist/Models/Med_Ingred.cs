using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class Med_Ingred
    {
        public int MedicationID { get; set; }
        public int ActiveIngredientID { get; set; }
        public int ActiveStrength { get; set; }
        public string Description { get; set; }
    }
}
