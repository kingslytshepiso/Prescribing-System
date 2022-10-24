namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PrescLineAnalytic
    {
        public int ID { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int IgnoredCount { get; set; }
        public int RejectedCount { get; set; }
        public int DispensedCount { get; set; }
        public int PharmacistID { get; set; }
        protected PharmacistDbcontext data = new PharmacistDbcontext();
        public PrescLineAnalytic()
        {
            var prescriptions = data.GetAllPrescriptions();
            var prescriptionLines = data.GetAllPrescriptionLines();
            foreach (var p in prescriptions)
            {
                foreach (var l in prescriptionLines)
                {
                    if (p.PrescriptionID == l.PrescriptionID)
                    {
                        if (p.Status == "Active")
                            ActiveCount++;
                        else
                            InactiveCount++;
                    }
                }
            }
        }
    }
}
