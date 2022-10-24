using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;
using System.ComponentModel.DataAnnotations;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class IndexViewModel
    {
        public User LoggedUser { get; set; }
        public PharmacistUser User { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public PrescLineAnalytic PrescriptionLineAnalytics = new PrescLineAnalytic();
        public PrescMedicationAnalytic MedicationAnalytics = new PrescMedicationAnalytic();
        protected PharmacistDbcontext data = new PharmacistDbcontext();
        public IndexViewModel()
        {
            var id = UserSingleton.GetLoggedUser().UserId;
            PrescriptionLineAnalytics = data.GetPrescLinesAnalyticWithPharmacistId(UserSingleton.GetLoggedUser().UserId);
        }
        
        [Required]
        [MaxLength(13, ErrorMessage ="Please enter not more than 13 characters.")]
        [MinLength(13, ErrorMessage = "Please enter atleast 13 characters.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter numbers only.")]
        public string IdNumber { get; set; }
        //OTHER PROPERTIES REQUIRED BY THE VIEW;
    }
}
