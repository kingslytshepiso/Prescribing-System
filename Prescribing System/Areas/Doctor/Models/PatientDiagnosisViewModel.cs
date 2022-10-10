using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class PatientDiagnosisViewModel
    {
        public Prescribing_System.Models.Patient UserPatient { get; set; }
        public User UserDetails { get; set; }
        public DoctorDbContext data = new DoctorDbContext();
        public Doctor Doctor { get; set; }
        public AcuteDisease AcuteDisease { get; set; }
        public ChronicDisease ChronicDisease { get; set; }
        public AcuteMedication AcuteMedication { get; set; }
        public ChronicMedication ChronicMedication { get; set; }
        public Allergy Allergy { get; set; }
        public Disease Disease { get; set; }
        public ActiveIngredient ActiveIngredient { get; set; }
        public Medication Medication { get; set; }
        public Dosage Dosage { get; set; }
        public List<Medication> Medications { get; set; }
        public List<ChronicDisease> ChronicDiseases { get; set; }
        public List<AcuteDisease> AcuteDiseases { get; set; }
        public List<AcuteMedication> AcuteMedications { get; set; }
        public List<ChronicMedication> ChronicMedications { get; set; }
        public List<Disease> Diseases { get; set; }
        public List<ActiveIngredient> ActiveIngredients { get; set; }
        public List<Allergy> Allergies { get; set; }
        public List<ListPatientChronicDisease> chronicDiseases = new List<ListPatientChronicDisease>();

        public PatientDiagnosisViewModel()
        {
            Diseases = data.GetAllDiseases();
            Medications = data.GetAllMeds();
            ActiveIngredients = data.GetAllActiveIngredients();
        }
        public int OverallCount = 0;
        public int CurrentPage = 1;
    }



}
