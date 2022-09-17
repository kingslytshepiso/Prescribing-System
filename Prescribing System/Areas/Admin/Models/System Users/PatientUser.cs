﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Prescribing_System.Areas.Admin.Models.System_Users
{
    public class PatientUser
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Please enter first name.")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name.")]
        [StringLength(30)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter ID number.")]
        [Remote("CheckID", "Validation")]
        public string IdNumber { get; set; }
        public char Gender { get; set; } = 'O';
        [Required(ErrorMessage = "Please enter email address.")]
        [Remote("CheckEmail", "Validation")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter contact number.")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Please enter Address line 1")]
        public string AddressLine1 { get; set; }
        //Address line 2 is not required
        public string AddressLine2 { get; set; }
        [Required()]
        public int SuburbID { get; set; }
        public int ProvinceID { get; set; }
    }
}
