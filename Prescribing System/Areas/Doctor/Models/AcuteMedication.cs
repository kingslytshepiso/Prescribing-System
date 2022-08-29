using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class AcuteMedication
    {
        public int AcuteMedicationID { get; set; }
        public int PatientID { get; set; }
        public int MedicationID { get; set; }
    }
}
