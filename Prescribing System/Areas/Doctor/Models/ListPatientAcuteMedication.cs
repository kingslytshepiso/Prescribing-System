using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class ListPatientAcuteMedication
    {
        public Prescribing_System.Models.Patient Patient { get; set; }
        public Medication Medication { get; set; }
        public AcuteMedication AcuteMedication { get; set; }
    }
}
