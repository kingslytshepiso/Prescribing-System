using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class IndexViewModel
    {
        public User LoggedUser { get; set; }
        public Prescribing_System.Models.Patient Patient { get; set; }
        //OTHER PROPERTIES REQUIRED BY THE VIEW;
    }
}
