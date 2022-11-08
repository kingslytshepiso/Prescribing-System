using System.Collections.Generic;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class AddPrescriptionLineViewModel
    {
        public List<GenericPrescriptionLine> DataList = new List<GenericPrescriptionLine>();
        public List<PrescriptionLine> Lines = new List<PrescriptionLine>();
        public PrescriptionLine line { get; set; }
        public AddPrescriptionLineViewModel()
        {

        }
    }
}
