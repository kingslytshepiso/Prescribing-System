﻿namespace Prescribing_System.Areas.Admin.Models.System_Objects
{
    public class MedicationInteraction
    {
        public int InteractionID { get; set; }
        public int FirstInteractor { get; set; }
        public int ScndInteractor { get; set; }
        public string EffectDescription { get; set; }
        public string Status { get; set; }
        public int TypeID { get; set; }
        protected AdminDbContext Data = new AdminDbContext();
        
    }
    public class MedInteractionGeneric : MedicationInteraction
    {
        public string TypeDescription { get; set; }
        public InteractionType Type { get; set; }
        public InteractionType GetInterationType()
        {
            return Data.GetAllInteractionTypes().Find(x => x.TypeID == TypeID);
        }
        public ActiveIngredient GetFirstInteractor()
        {
            return Data.GetActIngreWithId(FirstInteractor);
        }
        public string GetScndInteractor()
        {
            switch (TypeID)
            {
                case 1: return Data.GetActIngreWithId(ScndInteractor).Name;
                case 2: return "Food";
                case 3: return Data.GetDiseaseWithId(ScndInteractor).Name;
                default: return "interactor";
            }
        }
    }

    public class Med_MedInteraction : MedInteractionGeneric
    {
        public Medication_Ingredient FirstIngredient { get; set; }
        public Medication_Ingredient ScndIngredient { get; set; }
    }
    public class Med_Food : MedInteractionGeneric
    {
        public Medication_Ingredient Medication { get; set; }
        public string Food { get; set; }
    }
    public class Med_Disease : MedInteractionGeneric
    {
        public Medication_Ingredient Medication { get; set; }
        public Disease Disease { get; set; }
    }
}
