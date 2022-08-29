using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class DoctorDbContext
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

        public PatientListContex GetAllPatientsWithPaging(int pageNumber, int pageSize)
        {
            PatientListContex patients = new PatientListContex();
            connection();
            dbCmd = new SqlCommand("GetAllPatientsWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                patients.PatientList = new List<Prescribing_System.Models.Patient>();
                patients.OverallCount = ds.Tables.Count;
                patients.CurrentPage = pageNumber;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    patients.PatientList.Add(
                        new Prescribing_System.Models.Patient()
                        {
                            PatientID = Convert.ToInt32(current["PatientID"].ToString()),
                            FirstName = Convert.ToString(current["FirstName"].ToString()),
                            LastName = Convert.ToString(current["LastName"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            AddressLine2 = Convert.ToString(current["AddressLine2"].ToString())
                        });
                }
                return patients;
            }
            else
            {
                return patients;
            }
        }
    }
}
