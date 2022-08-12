using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class Pharmacy
    {
        public int PharmacyID { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string LicenceNo { get; set; }
        public string Img { get; set; }
        public int SuburbID { get; set; }
    }
}
