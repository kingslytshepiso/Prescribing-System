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
            string constring = "Data Source = sict-sql.mandela.ac.za; Initial Catalog = GRP-4-10-E-Prescribing; Integrated Security = false; User ID = GRP-4-10; Password = grp-4-10-soit2022";
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
        public List<Suburb> GetAllSuburbs()
        {
            List<Suburb> Suburbs = new List<Suburb>();
            connection();
            dbCmd = new SqlCommand("GetAllSuburbs", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    Suburbs.Add(
                        new Suburb()
                        {
                            SuburbID = Convert.ToInt32(current["SuburbID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            PostalCode = Convert.ToString(current["PostalCode"].ToString()),
                            CityID = Convert.ToInt32(current["CityID"].ToString())
                        });
                }
                return Suburbs;
            }
            else
            {
                return Suburbs;
            }
        }
        public List<City> GetAllCities()
        {
            List<City> Cities = new List<City>();
            connection();
            dbCmd = new SqlCommand("GetAllCities", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    Cities.Add(
                        new City()
                        {
                            CityID = Convert.ToInt32(current["CityID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            ProvID = Convert.ToInt32(current["ProvID"].ToString())
                        });
                }
                return Cities;
            }
            else
            {
                return Cities;
            }
        }
        public List<Province> GetAllProvinces()
        {
            List<Province> Provinces = new List<Province>();
            connection();
            dbCmd = new SqlCommand("GetAllProvinces", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    Provinces.Add(
                        new Province()
                        {
                            ProvID = Convert.ToInt32(current["ProvID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            Abbreviation = Convert.ToString(current["Abbreviation"].ToString()),
                        });
                }
                return Provinces;
            }
            else
            {
                return Provinces;
            }
        }
        public bool CheckID(string id)
        {
            connection();
            dbCmd = new SqlCommand("CheckID", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool CheckEmail(string email)
        {
            connection();
            dbCmd = new SqlCommand("CheckEmail", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@email", email);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public bool AddUser(RegisterViewModel model)
        {
            connection();
            dbCmd = new SqlCommand("RegisterPatient", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            //Patient table columns
            dbCmd.Parameters.AddWithValue("@FirstName", model.UserPatient.FirstName);
            dbCmd.Parameters.AddWithValue("@LastName", model.UserPatient.LastName);
            dbCmd.Parameters.AddWithValue("@IdNumber", model.UserPatient.IdNumber);
            dbCmd.Parameters.AddWithValue("@Gender", model.UserPatient.Gender);
            dbCmd.Parameters.AddWithValue("@EmailAddress", model.UserPatient.EmailAddress);
            dbCmd.Parameters.AddWithValue("@ContactNo", model.UserPatient.ContactNumber);
            dbCmd.Parameters.AddWithValue("@AddressLine1", model.UserPatient.AddressLine1);
            dbCmd.Parameters.AddWithValue("@AddressLine2", model.UserPatient.AddressLine2??"");
            dbCmd.Parameters.AddWithValue("@SuburbID", model.UserPatient.SuburbID);
            //User table columns
            dbCmd.Parameters.AddWithValue("@Username", model.UserPatient.EmailAddress);
            dbCmd.Parameters.AddWithValue("@Password", model.UserDetails.Password);
            dbCmd.Parameters.AddWithValue("@Role", model.UserDetails.Role);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            else
                return false;
        }
    }
}
