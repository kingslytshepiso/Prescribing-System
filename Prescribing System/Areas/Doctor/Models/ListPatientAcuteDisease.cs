namespace Prescribing_System.Areas.Doctor.Models
{
    public class ListPatientAcuteDisease
    {
        public Prescribing_System.Models.Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Disease Disease { get; set; }
        public AcuteDisease AcuteDisease { get; set; }
    }
}
