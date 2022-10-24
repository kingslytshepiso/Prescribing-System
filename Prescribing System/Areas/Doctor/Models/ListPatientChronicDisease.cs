namespace Prescribing_System.Areas.Doctor.Models
{
    public class ListPatientChronicDisease
    {

        public Prescribing_System.Models.Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public ChronicDisease ChronicDisease { get; set; }
        public Disease Disease { get; set; }
        public Medication Medication { get; set; }

    }
}
