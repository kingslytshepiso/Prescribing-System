using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PrescriptionLine
    {
        public int LineID { get; set; }
        public int Quantity { get; set; }
        public string Instruction { get; set; }
        public int RepeatNo { get; set; }
        public int RepeatLeft { get; set; }
        public string Status { get; set; }
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public int PharmacyID { get; set; }
    }
}
