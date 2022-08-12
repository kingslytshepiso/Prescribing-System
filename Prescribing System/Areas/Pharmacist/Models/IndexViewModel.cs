using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class IndexViewModel
    {
        public User LoggedUser { get; set; }
        public PharmacistUser User { get; set; }
        public Pharmacy Pharmacy { get; set; }
        //OTHER PROPERTIES REQUIRED BY THE VIEW;
    }
}
