using System.ComponentModel.DataAnnotations;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class MedicalPractice
    {
        public int MedPracId { get; set; }
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter contact number.")]
        [StringLength(15)]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "please enter email address.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter practice number")]
        public string PracticeNo { get; set; }
        [Required(ErrorMessage = "Please enter address line")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int SuburbId { get; set; }
    }
}
