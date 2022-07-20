using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Prescribing_System.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Display(Description = "Username or ID number")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Description = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm password.")]
        [Compare("Password",ErrorMessage = "Passwords dont match.")]
        [DataType(DataType.Password)]
        public string ConfPassword { get; set; }
        public string Role { get; set; }
    }
}
