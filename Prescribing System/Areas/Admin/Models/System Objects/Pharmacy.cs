using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Admin.Models.System_Objects
{
    public class Pharmacy
    {
        public int PharmacyId { get; set; }
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter contact number.")]
        [StringLength(15)]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "please enter email address.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter license number")]
        public string LicenseNo { get; set; }
        [Required(ErrorMessage = "Please enter address line")]
        public string AddressLine1 { get; set; }
        public string Addressline2 { get; set; }
        public int SuburbId { get; set; }
        public string Image { get; set; }
        public int PharmacistID { get; set; }
    }
    public class PharmacyGeneric : Pharmacy
    {
        public string SuburbName { get; set; }
        public string PharmacistName { get; set; }
    }
}
