using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Prescribing_System.Areas.Patient.Models
{
    public class Pharmacy
    {
        public int PharmacyID { get; set; }
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(15)]
        public string Name { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please enter contact number.")]
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter email address.")]
        [Remote("CheckEmail", "Validation")]
        public string LicenceNo { get; set; }
        public string Img { get; set; }
        public int SuburbID { get; set; }
    }
}
