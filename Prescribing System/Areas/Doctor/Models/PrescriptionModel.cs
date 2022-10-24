namespace Prescribing_System.Areas.Doctor.Models
{
    public class PrescriptionModel
    {
        private static PrescriptionModel uniqueInstance;
        private static Prescription CurrentPrescription;
        private PrescriptionModel() { }
        public static PrescriptionModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new PrescriptionModel();
            return uniqueInstance;
        }
        public static void SetPrescription(Prescription prescription)
        {
            CurrentPrescription = prescription;
        }
        public static Prescription GetPrescription()
        {
            if (CurrentPrescription == null)
                return new Prescription();
            else
                return CurrentPrescription;
        }
    }
}
