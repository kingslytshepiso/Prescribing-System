﻿namespace Prescribing_System.Areas.Admin.Models.System_Objects
{
    public class Medication_Ingredient
    {
        public int MedIngreID { get; set; }
        public int MedicationID { get; set; }
        public int ActiveIngredientID { get; set; }
        public int ActiveStrength { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        protected AdminDbContext data = new AdminDbContext();
        public ActiveIngredient GetActiveIngredient()
        {
            return data.GetActIngreWithId(this.ActiveIngredientID);
        }
    }
    public class Med_IngreGeneric : Medication_Ingredient
    {
        public Medication Medication { get; set; }
        public ActiveIngredient Ingredient { get; set; }    
    }
}
