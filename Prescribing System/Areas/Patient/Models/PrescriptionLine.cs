namespace Prescribing_System.Areas.Patient.Models
{
    public class PrescriptionLine
    {
        public int LineID { get; set; }
        public int Quantity { get; set; }
        public string Instruction { get; set; }
        public int RepeatNo { get; set; }
        public int RepeatLeftNo { get; set; }
        public string Status { get; set; }
        public int DosageID { get; set; }
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public int PharmacyID { get; set; }
        public string NameOfPharmarcy { get; set; }
        protected PatientDbcontext Data = new PatientDbcontext();
        public Medication GetMed()
        {
            return Data.GetAllMeds().Find(x => x.MedicationID == MedicationID);
        }
        public Pharmacy GetPharmacy()
        {
            return Data.GetPharmacyWithId(PharmacyID);
        }
    }
}
