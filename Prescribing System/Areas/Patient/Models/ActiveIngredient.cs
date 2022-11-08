using System.ComponentModel.DataAnnotations;
namespace Prescribing_System.Areas.Patient.Models
{
    public class ActiveIngredient
    {
        public int ActiveIngreID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(15)]
        public string Description { get; set; }
        
    }
}
