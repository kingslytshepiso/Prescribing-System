using System.ComponentModel.DataAnnotations;
namespace Prescribing_System.Areas.Patient.Models
{
    public class Dosage
    {
        public int DosageID { get; set; }
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(15)]
        public string FormName { get; set; }
    }
}
