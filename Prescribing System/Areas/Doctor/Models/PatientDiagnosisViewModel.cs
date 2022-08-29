using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientDiagnosisViewModel
    {
        public Prescribing_System.Models.Patient UserPatient { get; set; }
        public User UserDetails { get; set; }
        public GlobalDbContext data = new GlobalDbContext();

    }
}
