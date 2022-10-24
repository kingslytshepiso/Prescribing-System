using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class Med_Ingred
    {
        public int MedIngreId { get; set; }
        public int MedicationID { get; set; }
        public int ActiveIngredientID { get; set; }
        public double ActiveStrength { get; set; }
        public string Description { get; set; }
        protected PharmacistDbcontext data = new PharmacistDbcontext();
        public ActiveIngredient GetActiveIngredient()
        {
            return data.GetActIngreWithId(this.ActiveIngredientID);
        }
        public Medication GetMedication()
        {
            return data.GetAllMeds().Find(x => x.MedicationID == this.MedicationID);
        }
    }
}
