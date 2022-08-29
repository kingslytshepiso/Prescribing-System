using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class Disease
    {
        public int DiseaseID { get; set; }
        public string DiseaseName { get; set; }
    }
}
