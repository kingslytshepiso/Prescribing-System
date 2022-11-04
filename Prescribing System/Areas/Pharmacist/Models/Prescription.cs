﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public DateTime Date { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string Status { get; set; }
        protected PharmacistDbcontext data = new PharmacistDbcontext();
        public bool HasLines() => data.GetPrescLinesWithPrescId(PrescriptionID).Count > 0;
    }
}
