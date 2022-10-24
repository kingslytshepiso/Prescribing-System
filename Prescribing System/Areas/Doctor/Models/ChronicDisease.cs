using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;
using System.Xml.Linq;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class ChronicDisease
    {
        public int ChronicDiseaseID { get; set; }
        public DateTime Date { get; set; }
        public int PatientID { get; set; }
        public int DiseaseID { get; set; }
        protected DoctorDbContext Data = new DoctorDbContext();
        public Disease GetChronicDiseases()
        {
            return Data.GetAllDiseases().Find(x => x.DiseaseID == DiseaseID);
        }
    }
}
