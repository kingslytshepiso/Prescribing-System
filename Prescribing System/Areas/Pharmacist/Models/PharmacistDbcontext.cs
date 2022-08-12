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
        DataSet ds;
        public void connection()
        {
            string constring = "Data Source = localhost; Initial Catalog = E-Prescribing; Integrated Security = SSPI";
            conn = new SqlConnection(constring);
        }
        public PharmacistUser GetPharmacistWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetUserWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PharmacistUser user = new PharmacistUser();
            if (dt.Rows.Count > 0)
            {
                //user.PharmacistId = Convert.ToInt32(dt.Rows[0]["PharmacistID"].ToString());
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
    }
}
