using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class CurrentDoctorVisit
    {
        public int VistID { get; set; }
        [Required(ErrorMessage = "Please enter Reason of Visit.")]
        [StringLength(30)]
        public string ReasonOfVist { get; set; }
        [Required(ErrorMessage = "Please enter this field.")]
        [StringLength(30)]
        public string Whathurts { get; set; }
        [Required(ErrorMessage = "Please enter the symptoms of the patient.")]
        [StringLength(30)]
        public string Symptoms { get; set; }
        [Required(ErrorMessage = "Please enter this field.")]
        [StringLength(3)]
        public string SymptomDurtion { get; set; }
        public string VisitDate { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }

        protected DoctorDbContext Data = new DoctorDbContext();
        public Doctor GetDoctors()
        {
            return Data.GetDoctors().Find(x => x.DoctorId == DoctorID);
        }
    }


}
