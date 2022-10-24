using System.Collections.Generic;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class AddPrescriptionLineViewModel
    {
        public List<GenericPrescriptionLine> DataList;
        public PrescriptionLine line { get; set; }

    }
}
