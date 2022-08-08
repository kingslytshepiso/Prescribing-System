using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models.System_Objects;
using Prescribing_System.Areas.Admin.Models.System_Users;

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
        public List<MedicalPractice> GetAllMedPracs()
        {
            List<MedicalPractice> Practices = new List<MedicalPractice>();
            connection();
            dbCmd = new SqlCommand("GetAllMedPracs", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    Practices.Add(
                        new MedicalPractice()
                        {
                            MedPracId = Convert.ToInt32(current["MedPracID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            ContactNo = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            PracticeNo = Convert.ToString(current["PracticeNo"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            Addressline2 = Convert.ToString(current["AddressLine2"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString())
                        });
                }
            }
            return Practices;
        }
        public List<Pharmacy> GetAllPharmacies()
        {
            List<Pharmacy> Pharmacies = new List<Pharmacy>();
            connection();
            dbCmd = new SqlCommand("GetAllPharmacies", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    Pharmacies.Add(
                        new Pharmacy()
                        {
                            PharmacyId = Convert.ToInt32(current["PharmacyID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            ContactNo = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            LicenceNo = Convert.ToString(current["LicenceNo"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            Addressline2 = Convert.ToString(current["AddressLine2"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString())
                        });
                }
            }
            return Pharmacies;
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
        public bool UpdateUser(User user)
        {
            connection();
            dbCmd = new SqlCommand("UpdateUser", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@UserId", user.UserId);
            dbCmd.Parameters.AddWithValue("@Username", user.Username);
            dbCmd.Parameters.AddWithValue("@Password", user.Password);
            dbCmd.Parameters.AddWithValue("@Role", user.Role);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool AddDoctor(AddUserViewModel model)
        {
            connection();
            DoctorUser doctor = (DoctorUser)model.SelectedUser;
            var emailExist = CheckEmail(doctor.EmailAddress);
            if (!emailExist)
            {
                dbCmd = new SqlCommand("AddDoctor", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                //Patient table columns
                dbCmd.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                dbCmd.Parameters.AddWithValue("@LastName", doctor.LastName);
                dbCmd.Parameters.AddWithValue("@EmailAddress", doctor.EmailAddress);
                dbCmd.Parameters.AddWithValue("@ContactNo", doctor.ContactNumber);
                dbCmd.Parameters.AddWithValue("@AddressLine1", doctor.AddressLine1);
                dbCmd.Parameters.AddWithValue("@AddressLine2", doctor.AddressLine2);
                dbCmd.Parameters.AddWithValue("@SuburbID", doctor.SuburbID);
                dbCmd.Parameters.AddWithValue("@HighestQualification", doctor.HighestQual);
                dbCmd.Parameters.AddWithValue("@MedPracID", doctor.MedPracId);
                //User table columns
                dbCmd.Parameters.AddWithValue("@Username", doctor.EmailAddress);
                dbCmd.Parameters.AddWithValue("@Password", model.UserDetails.Password);
                dbCmd.Parameters.AddWithValue("@Role", model.UserDetails.Role);
                conn.Open();
                int i = dbCmd.ExecuteNonQuery();
                conn.Close();
                if (i >= 1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool AddPatient(AddUserViewModel model)
        {
            connection();
            PatientUser patient = (PatientUser)model.SelectedUser;
            var emailExist = CheckEmail(patient.EmailAddress);
            if (!emailExist)
            {
                dbCmd = new SqlCommand("AddPatient", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                //Patient table columns
                dbCmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                dbCmd.Parameters.AddWithValue("@LastName", patient.LastName);
                dbCmd.Parameters.AddWithValue("@IdNumber", patient.IdNumber);
                dbCmd.Parameters.AddWithValue("@EmailAddress", patient.EmailAddress);
                dbCmd.Parameters.AddWithValue("@ContactNo", patient.ContactNumber);
                dbCmd.Parameters.AddWithValue("@AddressLine1", patient.AddressLine1);
                dbCmd.Parameters.AddWithValue("@AddressLine2", patient.AddressLine2);
                dbCmd.Parameters.AddWithValue("@SuburbID", patient.SuburbID);
                //User table columns
                dbCmd.Parameters.AddWithValue("@Username", patient.EmailAddress);
                dbCmd.Parameters.AddWithValue("@Password", model.UserDetails.Password);
                dbCmd.Parameters.AddWithValue("@Role", model.UserDetails.Role);
                conn.Open();
                int i = dbCmd.ExecuteNonQuery();
                conn.Close();
                if (i >= 1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool AddPharmacist(AddUserViewModel model)
        {
            connection();
            PharmacistUser pharmacist = (PharmacistUser)model.SelectedUser;
            var emailExist = CheckEmail(pharmacist.EmailAddress);
            if (!emailExist)
            {
                dbCmd = new SqlCommand("AddPharmacist", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                //Patient table columns
                dbCmd.Parameters.AddWithValue("@FirstName", pharmacist.FirstName);
                dbCmd.Parameters.AddWithValue("@LastName", pharmacist.LastName);
                dbCmd.Parameters.AddWithValue("@EmailAddress", pharmacist.EmailAddress);
                dbCmd.Parameters.AddWithValue("@ContactNo", pharmacist.ContactNumber);
                dbCmd.Parameters.AddWithValue("@AddressLine1", pharmacist.AddressLine1);
                dbCmd.Parameters.AddWithValue("@AddressLine2", pharmacist.AddressLine2);
                dbCmd.Parameters.AddWithValue("@SuburbID", pharmacist.SuburbId);
                dbCmd.Parameters.AddWithValue("@PharmacyID", pharmacist.PharmacyId);
                //User table columns
                dbCmd.Parameters.AddWithValue("@Username", pharmacist.EmailAddress);
                dbCmd.Parameters.AddWithValue("@Password", model.UserDetails.Password);
                dbCmd.Parameters.AddWithValue("@Role", model.UserDetails.Role);
                conn.Open();
                int i = dbCmd.ExecuteNonQuery();
                conn.Close();
                if (i >= 1)
                {
                    return true;
                }
            }
            return false;
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
        public SearchIndexModel SearchForPatients(string search)
        {
            connection();
            SearchIndexModel results = new SearchIndexModel();
            results.Keyword = search;
            dbCmd = new SqlCommand("SearchForPatients", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Search", search);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    results.Objects.Add(
                        new SearchObject {
                            Id = Convert.ToInt32(current["PatientID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString())
                        });
                }
                results.Type = "Patients";
            }
            return results;
        }
        public SearchIndexModel SearchForDoctors(string search)
        {
            connection();
            SearchIndexModel results = new SearchIndexModel();
            results.Keyword = search;
            dbCmd = new SqlCommand("SearchForDoctors", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Search", search);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    results.Objects.Add(
                        new SearchObject
                        {
                            Id = Convert.ToInt32(current["DoctorID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString())
                        });
                }
                results.Type ="Doctors";
            }
            return results;
        }
        public SearchIndexModel SearchForPharmacists(string search)
        {
            connection();
            SearchIndexModel results = new SearchIndexModel();
            results.Keyword = search;

            dbCmd = new SqlCommand("SearchForPharmacists", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Search", search);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    results.Objects.Add(
                        new SearchObject
                        {
                            Id = Convert.ToInt32(current["PharmacistID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString())
                        });
                }
                results.Type = "Pharmacist";
            }
            return results;
        }
        public SearchIndexModel SearchForMedication(string search)
        {
            connection();
            SearchIndexModel results = new SearchIndexModel();
            results.Keyword = search;

            dbCmd = new SqlCommand("SearchForDoctors", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Search", search);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    results.Objects.Add(
                        new SearchObject
                        {
                            Id = Convert.ToInt32(current["MedicationID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString())
                        });
                }
                results.Type = "Medication";
            }
            return results;
        }
        public List<SearchIndexModel> SearchAll(string keyword)
        {
            List<SearchIndexModel> results = new List<SearchIndexModel>();
            results.Add(SearchForMedication(keyword));
            results.Add(SearchForPharmacists(keyword));
            results.Add(SearchForDoctors(keyword));
            results.Add(SearchForPatients(keyword));
            return results;
        }
    }
}
