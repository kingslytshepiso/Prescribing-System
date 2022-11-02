using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
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
        public int SuburbID { get; set; }
        public int ProvinceID { get; set; }
        [Required(ErrorMessage = "Please enter highest qualification")]
        public string HighestQual { get; set; }
        public int MedPracId { get; set; }
        public int CityID { get; set; }
        protected DoctorDbContext data = new DoctorDbContext();
        public MedicalPractice GetMedicalPractice()
        {
            return data.GetMedicalPracticeWithId(MedPracId);
        }
    }
}
