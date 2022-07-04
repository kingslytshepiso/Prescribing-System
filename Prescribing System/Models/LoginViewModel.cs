using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Prescribing_System.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
