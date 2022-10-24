namespace Prescribing_System.Areas.Doctor.Models
{
    public interface IMedication
    {
        public Medication Medication { get; set; }
        public ActiveIngredient ActiveIngredient { get; set; }
        public Schedule Schedule { get; set; }
        public Dosage Dosage { get; set; }
    }
}
