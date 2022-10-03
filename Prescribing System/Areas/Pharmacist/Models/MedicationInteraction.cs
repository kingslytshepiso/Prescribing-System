namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class MedicationInteraction
    {
        public int InteractionID { get; set; }
        public int FirstInteractor { get; set; }
        public int ScndInteractor { get; set; }
        public string EffectDescription { get; set; }
        public int TypeID { get; set; }
        protected PharmacistDbcontext Data = new PharmacistDbcontext();
        public ActiveIngredient GetFirstInteractor()
        {
            return Data.GetActIngreWithId(FirstInteractor);
        }
        public ActiveIngredient GetScndInteractor()
        {
            return Data.GetActIngreWithId(ScndInteractor);
        }
    }
}
