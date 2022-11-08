using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PharmacistUser
    {
        public int PharmacistId { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name.")]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter email address.")]
        [Remote("CheckEmail", "Validation")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter contact number.")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Please enter Address line 1")]
        public string AddressLine1 { get; set; }
        //Address line 2 is not required
        public string AddressLine2 { get; set; }
        public int PharmacyId { get; set; }
        public int SuburbId { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
    }
}
