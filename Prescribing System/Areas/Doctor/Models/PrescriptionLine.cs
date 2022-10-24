namespace Prescribing_System.Areas.Doctor.Models
{
    public class PrescriptionLine
    {
        public int PresciptionLineID { get; set; }
        public string Quantity { get; set; }
        public string Instruction { get; set; }
        public int RepeatNo { get; set; }
        public int RepeatLeftNo { get; set; }
        public string Status { get; set; }
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public int DosageID { get; set; }
        protected DoctorDbContext Data = new DoctorDbContext();
        public Medication GetMed()
        {
            return Data.GetAllMeds().Find(x => x.MedicationID == MedicationID);
        }
    }
}
