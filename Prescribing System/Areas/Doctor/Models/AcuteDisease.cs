using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class AcuteDisease
    {
        public int AcuteDiseaseID { get; set; }
        public string Date { get; set; }
        public int PatientID { get; set; }
        public int DiseaseID { get; set; }
    }
}
