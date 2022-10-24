using System.Collections;
using System.Collections.Generic;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PrescMedicationAnalytic
    {
        public List<Medication> MedList { get; set; }
        public Dictionary<string, int> MedCountList { get; set; }
        public List<PrescriptionLine> Lines { get; set; }
        protected PharmacistDbcontext data = new PharmacistDbcontext();
        public PrescMedicationAnalytic()
        {
            Lines = data.GetAllPrescriptionLines();
            MedCountList = new Dictionary<string, int>();
            foreach (var l in Lines)
            {
                var lineMed = l.GetActualMed();
                if (MedCountList.ContainsKey(lineMed.Name))
                {
                    MedCountList[lineMed.Name] = MedCountList[lineMed.Name] + 1;
                }
                else
                {
                    MedCountList.Add(lineMed.Name, 1);
                }
            }
        }
    }
}
