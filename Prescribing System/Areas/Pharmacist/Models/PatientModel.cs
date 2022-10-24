using Prescribing_System.Models;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PatientModel
    {
        private static PatientModel uniqueInstance;
        private static PatientUser CurrentUser;
        private PatientModel() { }
        public static PatientModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new PatientModel();
            return uniqueInstance;
        }
        public static void SetPatient(PatientUser patient)
        {
            CurrentUser = patient;
        }
        public static PatientUser GetPatient()
        {
            if (CurrentUser == null)
                return new PatientUser();
            return CurrentUser;
        }
        public static void Reset()
        {
            uniqueInstance = null;
            CurrentUser = null;
        }
        public static bool IsFound() => CurrentUser != null;
    }
}
