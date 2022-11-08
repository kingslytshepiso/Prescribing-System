using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Prescribing_System.Areas.Patient.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public DateTime Date { get; set; }
        public string YearStr { get; set; }
        public string MonthStr { get; set; }
        public string dayStr { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        [Required(ErrorMessage = "Please enter Doctor name.")]
        [StringLength(15)]
        public string Status { get; set; }
        public string PrescriptionDate { get; set; }

    }
}
