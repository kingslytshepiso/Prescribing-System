using System.ComponentModel.DataAnnotations;
namespace Prescribing_System.Areas.Patient.Models
{
    public class Disease
    {
        public int DiseaseID { get; set; }
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(15)]
        public string Name { get; set; }

    }
}
