
namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class ContraIndication
    {
        public int ContraIndicaID { get; set; } 
        public int ActiveIngredientID { get; set; } 
        public string Description { get; set; }
        public int DiseaseID { get; set; }
        protected PharmacistDbcontext Data = new PharmacistDbcontext();
        public ActiveIngredient GetActiveIngredient()
        {
            return Data.GetActIngreWithId(ActiveIngredientID);
        }
        public Disease GetDisease()
        {
            return Data.GetDiseaseWithId(DiseaseID);
        }
    }
    public class ContraIndicationGeneric : ContraIndication
    {
        public string ActiveIngredientName { get; set; }
        public string DiseaseName { get; set; }

    }
}
