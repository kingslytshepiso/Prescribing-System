using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Admin.Models
{
    public class AdminDbContext
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
        public UserListContext GetAllUsersWithPaging(int pageNumber, int pageSize)
        {
            UserListContext users = new UserListContext();
            connection();
            dbCmd = new SqlCommand("GetAllUsersWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                users.DataList = new List<User>();
                users.OverallCount = ds.Tables.Count;
                users.CurrentPage = pageNumber;
                foreach (DataRow current in ds.Tables[pageNumber-1].Rows)
                {
                    users.DataList.Add(
                        new User()
                        {
                            UserId = Convert.ToInt32(current["UserID"].ToString()),
                            Username = Convert.ToString(current["Username"].ToString()),
                            Password = Convert.ToString(current["Password"].ToString()),
                            Role = Convert.ToString(current["Role"].ToString())
                        });
                }
                return users;
            }
            else
            {
                return users;
            }
        }
        public User GetUserWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetUserWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
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
