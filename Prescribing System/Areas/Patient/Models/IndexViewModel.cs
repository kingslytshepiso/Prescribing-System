using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Patient.Models
{
    public class IndexViewModel
    {
        public User LoggedUser { get; set; }
        public PatientUser User { get; set; }
        public PatientUser Patient { get; set; }
        //OTHER PROPERTIES REQUIRED BY THE VIEW;
    }
}
