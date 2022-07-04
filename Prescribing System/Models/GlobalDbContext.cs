using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Prescribing_System.Models
{
    public class GlobalDbContext 
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
        public bool Login(string username, string password)
        {
            connection();
            dbCmd = new SqlCommand("Login", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@username", username);
            dbCmd.Parameters.AddWithValue("@password", password);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public User GetUserWithUsername(string username)
        {
            connection();
            dbCmd = new SqlCommand("GetUserWithUsername", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@username", username);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new User()
                {
                    UserId = Convert.ToInt32(dt.Rows[0]["UserID"].ToString()),
                    Username = Convert.ToString(dt.Rows[0]["Username"].ToString()),
                    Password = Convert.ToString(dt.Rows[0]["Password"].ToString()),
                    Role = Convert.ToString(dt.Rows[0]["Role"].ToString())
                };
            }
            else
                return new User();
        }
    }
}
