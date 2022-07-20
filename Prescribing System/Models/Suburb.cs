using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Models
{
    public class Suburb
    {
        public int SuburbID { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public int CityID { get; set; }
    }
}
