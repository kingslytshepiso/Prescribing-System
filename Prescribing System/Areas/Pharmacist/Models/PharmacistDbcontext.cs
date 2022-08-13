using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PharmacistDbcontext
    {
        private SqlConnection conn;
        SqlCommand dbCmd;
        SqlDataAdapter dbAdapter;
        DataTable dt;
        public void connection()
        {
            string constring = "Data Source = localhost; Initial Catalog = E-Prescribing; Integrated Security = SSPI";
            conn = new SqlConnection(constring);
        }
        public PharmacistUser GetPharmacistWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPharmacistWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PharmacistUser user = new PharmacistUser();
            if (dt.Rows.Count > 0)
            {
                user.PharmacistId = Convert.ToInt32(dt.Rows[0]["PharmacistID"].ToString());
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"].ToString());
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"].ToString());
                user.ContactNumber = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                user.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                user.AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString());
                user.AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString());
                user.PharmacyId = Convert.ToInt32(dt.Rows[0]["PharmacyID"].ToString());
                user.SuburbId = Convert.ToInt32(dt.Rows[0]["SuburbID"].ToString());
            }
            return user;
        }
        public DoctorUser GetDoctorWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetDoctorWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            DoctorUser user = new DoctorUser();
            if (dt.Rows.Count > 0)
            {
                user.DoctorId = Convert.ToInt32(dt.Rows[0]["DoctorID"].ToString());
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"].ToString());
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"].ToString());
                user.ContactNumber = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                user.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                user.HighestQual = Convert.ToString(dt.Rows[0]["HighestQualification"].ToString());
                user.AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString());
                user.AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString());
                user.SuburbID = Convert.ToInt32(dt.Rows[0]["SuburbID"].ToString());
                user.MedPracId = Convert.ToInt32(dt.Rows[0]["MedPracID"].ToString());
            }
            return user;
        }
        public Pharmacy GetPharmacyWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPharmacyWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Pharmacy pharmacy = new Pharmacy();
            if (dt.Rows.Count > 0)
            {
                pharmacy.PharmacyID = Convert.ToInt32(dt.Rows[0]["PharmacyID"].ToString());
                pharmacy.Name = Convert.ToString(dt.Rows[0]["Name"].ToString());
                pharmacy.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                pharmacy.AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString());
                pharmacy.AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString());
                pharmacy.ContactNo = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                pharmacy.LicenceNo = Convert.ToString(dt.Rows[0]["LicenceNo"].ToString());
                pharmacy.SuburbID = Convert.ToInt32(dt.Rows[0]["SuburbID"].ToString());
            }
            return pharmacy;
        }
        public PatientUser GetPatientWithIdNo(string number)
        {
            connection();
            dbCmd = new SqlCommand("GetPatientWithIdNo", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", number);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PatientUser patient = new PatientUser();
            if (dt.Rows.Count > 0)
            {
                patient.PatientId = Convert.ToInt32(dt.Rows[0]["PatientID"].ToString());
                patient.FirstName = Convert.ToString(dt.Rows[0]["FirstName"].ToString());
                patient.LastName = Convert.ToString(dt.Rows[0]["LastName"].ToString());
                patient.IdNumber = Convert.ToString(dt.Rows[0]["IdNumber"].ToString());
                patient.ContactNumber = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                patient.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                patient.AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString());
                patient.AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString());
                patient.SuburbID = Convert.ToInt32(dt.Rows[0]["SuburbID"].ToString());
            }
            return patient;
        }
        public Prescription GetPrescriptionWithId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetPrescriptionWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Prescription prescription = null;
            if (dt.Rows.Count > 0)
            {
                prescription.PrescriptionID = Convert.ToInt32(dt.Rows[i]["PrescriptionID"].ToString());
                prescription.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                prescription.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                prescription.DoctorID = Convert.ToInt32(dt.Rows[i]["DoctorID"].ToString());
            }
            return prescription;
        }
        public List<Prescription> GetPrescriptionsWithId(int id)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var p = GetPrescriptionWithId(id, i);
                if (p == null)
                {
                    endOfLine = true;
                }
                else
                {
                    prescriptions.Add(p);
                    i++;
                }
            }
            return prescriptions;
        }
        public PrescriptionLine GetPrescLineWithId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetPrescriptionWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PrescriptionLine line = null;
            if (dt.Rows.Count > 0)
            {
                line.LineID = Convert.ToInt32(dt.Rows[i]["LineID"].ToString());
                line.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                line.Instruction = Convert.ToString(dt.Rows[i]["Instruction"].ToString());
                line.RepeatNo = Convert.ToInt32(dt.Rows[i]["RepeatNo"].ToString());
                line.RepeatLeft = Convert.ToInt32(dt.Rows[i]["RepeatLeft"].ToString());
                line.Status = Convert.ToString(dt.Rows[i]["Status"].ToString());
                line.PrescriptionID = Convert.ToInt32(dt.Rows[i]["PrescriptionID"].ToString());
                line.MedicationID = Convert.ToInt32(dt.Rows[i]["MedicationID"].ToString());
                line.PharmacyID = Convert.ToInt32(dt.Rows[i]["PharmacyID"].ToString());
            }
            return line;
        }
        public List<PrescriptionLine> GetPrescLinesWithId(int id)
        {
            List<PrescriptionLine> lines = new List<PrescriptionLine>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var p = GetPrescLineWithId(id, i);
                if (p == null)
                {
                    endOfLine = true;
                }
                else
                {
                    lines.Add(p);
                    i++;
                }
            }
            return lines;
        }
    }
}
