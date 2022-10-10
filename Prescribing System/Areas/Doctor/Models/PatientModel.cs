namespace Prescribing_System.Areas.Doctor.Models
{
	public class PatientModel
	{
        private static PatientModel uniqueInstance;
        private static Prescribing_System.Models.Patient CurrentUser;
        private PatientModel() { }
        public static PatientModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new PatientModel();
            return uniqueInstance;
        }
        public static void SetPatient(Prescribing_System.Models.Patient user)
        {
            CurrentUser = user;
        }
        public static Prescribing_System.Models.Patient GetPatient()
        {
            if (CurrentUser == null)
                return new Prescribing_System.Models.Patient();
            else
                return CurrentUser;
        }
    }
}
