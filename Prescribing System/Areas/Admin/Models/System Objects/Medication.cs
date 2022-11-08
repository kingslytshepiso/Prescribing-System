using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace Prescribing_System.Areas.Admin.Models.System_Objects
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public string Name { get; set; }
        public int DosageID { get; set; }
        public int ScheduleID { get; set; }
        public string TypeID { get; set; }
        public string Status { get; set; }
        public List<Medication_Ingredient> Ingredients { get; set; }
        protected AdminDbContext data = new AdminDbContext();
        public List<Medication_Ingredient> GetIngredients()
        {
            return data.GetAllMedicationIngredient().FindAll(x => x.MedicationID == this.MedicationID);
        }
    }
    public class MedicationGeneric : Medication
    {
        public string FormName { get; set; }
        public int ScheduleNo { get; set; }
    }
}
