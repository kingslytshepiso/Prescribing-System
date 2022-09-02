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
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
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
        public Act_IngreViewModel GetAllActIngreWithPaging(int pageNumber, int pageSize, string sort)
        {
            Act_IngreViewModel ingredients = new Act_IngreViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllActIngreWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                ingredients.DataList = new List<ActiveIngredient>();
                ingredients.OverallCount = ds.Tables.Count;
                ingredients.CurrentPage = pageNumber;
                ingredients.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    ingredients.DataList.Add(
                        new ActiveIngredient()
                        {
                            ActiveIngreID = Convert.ToInt32(current["ActiveIngreID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                        });
                }
                return ingredients;
            }
            else
            {
                return ingredients;
            }
        }
        public CndDgsisViewModel GetAllConditionDiagnosisWithPaging(int pageNumber, int pageSize,
            string sort)
        {
            CndDgsisViewModel model = new CndDgsisViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllConditionDiagnosisWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<ConditionDiagnosis>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new ConditionDiagnosis()
                        {
                            ConditionID = Convert.ToInt32(current["ConditionID"].ToString()),
                            Code = Convert.ToString(current["Code"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                            PatientID = Convert.ToInt32(current["PatientID"].ToString())
                        }); ;
                }
                return model;
            }
            else
            {
                return model;
            }
        }
        public CntrIndViewModel GetAllContraIndicationsWithPaging(int pageNumber, int pageSize,
            string sort)
        {
            CntrIndViewModel model = new CntrIndViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllContraIndicationsWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<ContraIndicationGeneric>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new ContraIndicationGeneric()
                        {
                            ContraIndicaID = Convert.ToInt32(current["ContraIndicaID"].ToString()),
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngredientID"].ToString()),
                            DiseaseID = Convert.ToInt32(current["DiseaseID"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                            ActiveIngredientName = Convert.ToString(current["ActiveIngredientName"].ToString()),
                            DiseaseName = Convert.ToString(current["DiseaseName"].ToString())
                        });
                }
                return model;
            }
            else
            {
                return model;
            }
        }

        public PatientUser GetPatientWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPatientWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PatientUser user = new PatientUser();
            if (dt.Rows.Count > 0)
            {
                user.PatientId = Convert.ToInt32(dt.Rows[0]["PatientID"].ToString());
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"].ToString());
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"].ToString());
                user.IdNumber = Convert.ToString(dt.Rows[0]["IdNumber"].ToString());
                user.ContactNumber = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                user.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                user.AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString());
                user.AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString());
                user.SuburbID = Convert.ToInt32(dt.Rows[0]["SuburbID"].ToString());
            }
            return user;
        }
        public List<PatientUser> GetAllPatients()
        {
            List<PatientUser> patients = new List<PatientUser>();
            connection();
            dbCmd = new SqlCommand("GetAllPatients", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    patients.Add(
                        new PatientUser()
                        {
                            PatientId = Convert.ToInt32(current["PatientID"].ToString()),
                            FirstName = Convert.ToString(current["FirstName"].ToString()),
                            LastName = Convert.ToString(current["LastName"].ToString()),
                            IdNumber = Convert.ToString(current["IdNumber"].ToString()),
                            ContactNumber = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            AddressLine2 = Convert.ToString(current["AddressLine2"].ToString()),
                            SuburbID = Convert.ToInt32(current["SuburbID"].ToString()),
                        });
                }
            }
            return patients;
        }
        public List<ICD_Code> GetAllICDCodes()
        {
            List<ICD_Code> codes = new List<ICD_Code>();
            connection();
            dbCmd = new SqlCommand("GetAllICDCodes", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    codes.Add(
                        new ICD_Code()
                        {
                            Code = Convert.ToString(current["Code"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString())
                        });
                }
            }
            return codes;
        }
        public List<ActiveIngredient> GetAllActiveIngredients()
        {
            List<ActiveIngredient> codes = new List<ActiveIngredient>();
            connection();
            dbCmd = new SqlCommand("GetAllActiveIngredients", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    codes.Add(
                        new ActiveIngredient()
                        {
                            ActiveIngreID = Convert.ToInt32(current["ActiveIngreID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString())
                        });
                }
            }
            return codes;
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
        public List<Disease> GetAllDiseases()
        {
            List<Disease> Pharmacies = new List<Disease>();
            connection();
            dbCmd = new SqlCommand("GetAllDiseases", conn);
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
                        new Disease()
                        {
                            DiseaseID = Convert.ToInt32(current["DiseaseID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
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
        public ActiveIngredient GetActIngreWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetActIngreWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new ActiveIngredient()
                {
                    ActiveIngreID = Convert.ToInt32(dt.Rows[0]["ActiveIngreID"].ToString()),
                    Name = Convert.ToString(dt.Rows[0]["Name"].ToString()),
                    Description = Convert.ToString(dt.Rows[0]["Description"].ToString()),
                };
            }
            else
                return new ActiveIngredient();
        }
        public ConditionDiagnosis GetCondDiagnosisWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetCondDiagnosisWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new ConditionDiagnosis()
                {
                    ConditionID = Convert.ToInt32(dt.Rows[0]["ConditionID"].ToString()),
                    Code = Convert.ToString(dt.Rows[0]["Code"].ToString()),
                    Description = Convert.ToString(dt.Rows[0]["Description"].ToString()),
                    PatientID = Convert.ToInt32(dt.Rows[0]["PatientID"].ToString())
                };
            }
            return new ConditionDiagnosis();
        }
        public Disease GetDiseaseWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetDiseaseWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new Disease()
                {
                    DiseaseID = Convert.ToInt32(dt.Rows[0]["DiseaseID"].ToString()),
                    Name = Convert.ToString(dt.Rows[0]["Name"].ToString())
                };
            }
            return new Disease();
        }
        public ContraIndication GetCntrIndWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetContrIndWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new ContraIndication()
                {
                    ContraIndicaID = Convert.ToInt32(dt.Rows[0]["ContraIndicaID"].ToString()),
                    ActiveIngredientID = Convert.ToInt32(dt.Rows[0]["ActiveIngredientID"].ToString()),
                    DiseaseID = Convert.ToInt32(dt.Rows[0]["DiseaseID"].ToString()),
                    Description = Convert.ToString(dt.Rows[0]["Description"].ToString())
                };
            }
            return new ContraIndication();
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
        public bool UpdateActiveIngredient(ActiveIngredient ingredient)
        {
            connection();
            dbCmd = new SqlCommand("UpdateActiveIngredient", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ActiveIngreID", ingredient.ActiveIngreID);
            dbCmd.Parameters.AddWithValue("@Name", ingredient.Name);
            dbCmd.Parameters.AddWithValue("@Description", ingredient.Description);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool AddActiveIngredient(ActiveIngredient ingredient)
        {
            connection();

            dbCmd = new SqlCommand("AddActiveIngredient", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Name", ingredient.Name);
            dbCmd.Parameters.AddWithValue("@Description", ingredient.Description);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool AddContraIndication(ContraIndication indication)
        {
            connection();

            dbCmd = new SqlCommand("AddContraIndication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ActiveIngredientID", indication.ActiveIngredientID);
            dbCmd.Parameters.AddWithValue("@DiseaseID", indication.DiseaseID);
            dbCmd.Parameters.AddWithValue("@Description", indication.Description);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool AddConditionDiagnosis(ConditionDiagnosis model)
        {
            connection();

            dbCmd = new SqlCommand("AddConditionDiagnosis", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Code", model.Code);
            dbCmd.Parameters.AddWithValue("@Description", model.Description);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool UpdateConditionDiagnosis(ConditionDiagnosis model)
        {
            connection();

            dbCmd = new SqlCommand("UpdateConditionDiagnosis", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ConditionID", model.ConditionID);
            dbCmd.Parameters.AddWithValue("@Code", model.Code);
            dbCmd.Parameters.AddWithValue("@Description", model.Description);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool UpdateContraIndication(ContraIndication model)
        {
            connection();

            dbCmd = new SqlCommand("UpdateContraIndication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ContraIndicaID", model.ContraIndicaID);
            dbCmd.Parameters.AddWithValue("@ActiveIngredientID", model.ActiveIngredientID);
            dbCmd.Parameters.AddWithValue("@DiseaseID", model.DiseaseID);
            dbCmd.Parameters.AddWithValue("@Description", model.Description);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool AddDoctor(AddDoctorViewModel model)
        {
            connection();
            DoctorUser doctor = model.User;
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
        public bool AddPatient(AddPatientViewModel model)
        {
            connection();
            PatientUser patient = model.User;
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
        public bool AddPharmacist(AddPharmacistViewModel model)
        {
            connection();
            PharmacistUser pharmacist = model.User;
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
                dbCmd.Parameters.AddWithValue("@AddressLine2", pharmacist.AddressLine2 ?? "");
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
