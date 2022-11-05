using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public string Name { get; set; }
        public int DosageID { get; set; }
        public int ScheduleID { get; set; }
        public string TypeID { get; set; }
        public int ActiveIngredientID { get; set; }
        protected PharmacistDbcontext Data = new PharmacistDbcontext();
        public List<Med_Ingred> GetIngredients()
        {
            return Data.GetAllMedicationIngredient().FindAll(x => x.MedicationID == MedicationID);
        }
    }
}
