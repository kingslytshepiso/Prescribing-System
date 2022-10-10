namespace Prescribing_System.Areas.Doctor.Models
{
    public class ListPatientChronicMedication
    {
        public Prescribing_System.Models.Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Medication Medication { get; set; }
        public ChronicMedication ChronicMedication { get; set; }


    }
}
