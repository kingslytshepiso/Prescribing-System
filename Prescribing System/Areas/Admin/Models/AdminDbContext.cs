using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models.System_Objects;
using Prescribing_System.Areas.Admin.Models.System_Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis.Differencing;

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
            string constring = "Data Source = sict-sql.mandela.ac.za; " +
                "Initial Catalog = GRP-4-10-E-Prescribing; Integrated Security = false;" +
                " User ID = GRP-4-10; Password = grp-4-10-soit2022";
            conn = new SqlConnection(constring);
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
        public List<Dosage> GetAllDosages()
        {
            List<Dosage> dosages = new List<Dosage>();
            connection();
            dbCmd = new SqlCommand("GetAllDosages", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    dosages.Add(
                        new Dosage()
                        {
                            DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            FormName = Convert.ToString(current["FormName"].ToString()),
                        });
                }
            }
            return dosages;
        }
        public List<Schedule> GetAllSchedules()
        {
            List<Schedule> schedules = new List<Schedule>();
            connection();
            dbCmd = new SqlCommand("GetAllSchedules", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    schedules.Add(
                        new Schedule()
                        {
                            ScheduleID = Convert.ToInt32(current["ScheduleID"].ToString()),
                            ScheduleNo = Convert.ToInt32(current["ScheduleNo"].ToString()),
                        });
                }
            }
            return schedules;
        }
        public ICDCodesViewModel GetAllICDCodesWithPaging(int pageNumber, int pageSize, string sort)
        {
            ICDCodesViewModel model = new ICDCodesViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllICDCodesWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<ICD_Code>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new ICD_Code()
                        {
                            Code = Convert.ToString(current["Code"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                        });
                }
            }
            return model;
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
        public List<InteractionType> GetAllInteractionTypes()
        {
            List<InteractionType> types = new List<InteractionType>();
            connection();
            dbCmd = new SqlCommand("GetAllInteractionTypes", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    types.Add(
                        new InteractionType()
                        {
                            TypeID = Convert.ToInt32(current["TypeID"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                        });
                }
                return types;
            }
            else
            {
                return types;
            }
        }
        public List<PharmacyGeneric> GetAllPharmacies()
        {
            List<PharmacyGeneric> Pharmacies = new List<PharmacyGeneric>();
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
                        new PharmacyGeneric()
                        {
                            PharmacyId = Convert.ToInt32(current["PharmacyID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            ContactNo = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            LicenseNo = Convert.ToString(current["LicenceNo"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            Addressline2 = Convert.ToString(current["AddressLine2"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString()),
                            PharmacistID = Convert.ToInt32(current["PharmacistID"].ToString()),
                            SuburbName = Convert.ToString(current["SuburbName"].ToString()),
                            PharmacistName = Convert.ToString(current["PharmacistName"].ToString()),
                        });
                }
            }
            return Pharmacies;
        }
        public PharmacistDataModel GetPharmacistAndUserWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPharmacistAndUserWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PharmacistDataModel model = new PharmacistDataModel();
            if (dt.Rows.Count > 0)
            {
                model.User.PharmacistId = Convert.ToInt32(dt.Rows[0]["PharmacistID"].ToString());
                model.User.FirstName = Convert.ToString(dt.Rows[0]["FirstName"].ToString());
                model.User.LastName = Convert.ToString(dt.Rows[0]["LastName"].ToString());
                model.User.ContactNumber = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                model.User.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                model.User.AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString());
                model.User.AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString());
                model.User.PharmacyId = Convert.ToInt32(dt.Rows[0]["PharmacyID"].ToString());
                model.User.SuburbId = Convert.ToInt32(dt.Rows[0]["SuburbID"].ToString());
                model.UserDetails.UserId = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
                model.UserDetails.Username = Convert.ToString(dt.Rows[0]["Username"].ToString());
                model.UserDetails.Password = Convert.ToString(dt.Rows[0]["Password"].ToString());
                model.UserDetails.Role = Convert.ToString(dt.Rows[0]["Role"].ToString());
            }
            return model;
        }
        public List<PharmacistUser> GetAllPharmacists()
        {
            List<PharmacistUser> pharmacists = new List<PharmacistUser>();
            connection();
            dbCmd = new SqlCommand("GetAllPharmacists", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    pharmacists.Add(
                        new PharmacistUser()
                        {
                            PharmacistId = Convert.ToInt32(current["PharmacistID"].ToString()),
                            FirstName = Convert.ToString(current["FirstName"].ToString()),
                            LastName = Convert.ToString(current["LastName"].ToString()),
                            ContactNumber = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            AddressLine2 = Convert.ToString(current["AddressLine2"].ToString()),
                            PharmacyId = Convert.ToInt32(current["PharmacyID"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString()),
                        });
                }
            }
            return pharmacists;
        }
        public List<Medication_Ingredient> GetAllMedicationIngredient()
        {
            List<Medication_Ingredient> meds = new List<Medication_Ingredient>();
            connection();
            dbCmd = new SqlCommand("GetAllMedicationIngredient", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    meds.Add(
                        new Medication_Ingredient()
                        {
                            MedIngreID = Convert.ToInt32(current["MedIngreID"].ToString()),
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngredientID"].ToString()),
                            ActiveStrength = Convert.ToDouble(current["ActiveStrength"].ToString()),
                            State = "Old",
                        });
                }
            }
            return meds;
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
        public List<MedicationType> GetAllMedTypes()
        {
            List<MedicationType> Pharmacies = new List<MedicationType>();
            connection();
            dbCmd = new SqlCommand("GetAllMedTypes", conn);
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
                        new MedicationType()
                        {
                            TypeID = Convert.ToString(current["TypeID"].ToString()),
                            TypeDescription = Convert.ToString(current["TypeDescription"].ToString()),
                        });
                }
            }
            return Pharmacies;
        }
        public UserViewModel GetAllUsersWithPaging(int pageNumber, int pageSize
            , string sort, string filter)
        {
            UserViewModel users = new UserViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllUsersWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            var strParameter = char.ToUpper(filter[0]) + filter.Substring(1);
            dbCmd.Parameters.AddWithValue("@Filter", strParameter);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            for(int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].Rows.Count == 0)
                {
                    ds.Tables.Remove(ds.Tables[i]);
                }
            }
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                users.DataList = new List<UserGenericModel>();
                users.OverallCount = ds.Tables.Count;
                users.CurrentPage = pageNumber;
                users.Sort = sort;
                users.Filter = filter;
                if (users.OverallCount <= pageNumber - 1)
                {
                    pageNumber = 1;
                    users.CurrentPage = pageNumber;
                }
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    users.DataList.Add(
                        new UserGenericModel()
                        {
                            UserId = Convert.ToInt32(current["UserID"].ToString()),
                            Username = Convert.ToString(current["Username"].ToString()),
                            Role = Convert.ToString(current["Role"].ToString()),
                            FirstName = Convert.ToString(current["FirstName"]).ToString(),
                            LastName = Convert.ToString(current["LastName"].ToString()),
                            Email = Convert.ToString(current["EmailAddress"].ToString()),
                            ContactNo = Convert.ToString(current["ContactNo"].ToString()),
                            Address = Convert.ToString(current["Address"].ToString())
                        });
                }
            }
            return users;
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
                        });
                }
                return ingredients;
            }
            else
            {
                return ingredients;
            }
        }
        public DoctorsViewModel GetAllDoctorsWithPaging(int pageNumber, int pageSize, string sort)
        {
            DoctorsViewModel doctors = new DoctorsViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllDoctorsWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                doctors.DataList = new List<DoctorUserGeneric>();
                doctors.OverallCount = ds.Tables.Count;
                doctors.CurrentPage = pageNumber;
                doctors.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    doctors.DataList.Add(
                        new DoctorUserGeneric()
                        {
                            DoctorId = Convert.ToInt32(current["DoctorID"].ToString()),
                            FirstName = Convert.ToString(current["FirstName"].ToString()),
                            LastName = Convert.ToString(current["LastName"].ToString()),
                            ContactNumber = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            HighestQual = Convert.ToString(current["HighestQualification"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            AddressLine2 = Convert.ToString(current["AddressLine2"].ToString()),
                            SuburbID = Convert.ToInt32(current["SuburbID"].ToString()),
                            SuburbName = Convert.ToString(current["SuburbName"].ToString()),
                            MedPracId = Convert.ToInt32(current["MedPracID"].ToString()),
                            MedPracName = Convert.ToString(current["MedPracName"].ToString()),
                            Status = Convert.ToString(current["Status"].ToString()),
                            StatusName = Convert.ToString(current["StatusName"].ToString()),
                            ProfileImage = Convert.ToString(current["ProfileImage"].ToString()),
                            HCRNumber = Convert.ToString(current["HCRNumber"].ToString())
                        });
                }
            }
            return doctors;
        }
        public PatientsViewModel GetAllPatientsWithPaging(int pageNumber, int pageSize, string sort)
        {
            PatientsViewModel patients = new PatientsViewModel();
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
                patients.DataList = new List<PatientUserGeneric>();
                patients.OverallCount = ds.Tables.Count;
                patients.CurrentPage = pageNumber;
                patients.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    patients.DataList.Add(
                        new PatientUserGeneric()
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
                            SuburbName = Convert.ToString(current["SuburbName"].ToString()),
                            Gender = Convert.ToString(current["Gender"].ToString()),
                            //GenderName = Convert.ToString(current["GenderName"].ToString()),
                            Status = Convert.ToString(current["Status"].ToString()),
                            //StatusName = Convert.ToString(current["StatusName"].ToString()),
                        });
                }
            }
            return patients;
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
        public InteractionViewModel GetAllInteractionsWithPaging(int pageNumber, int pageSize,
            string sort)
        {
            InteractionViewModel model = new InteractionViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllInteractionsWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<MedInteractionGeneric>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new MedInteractionGeneric()
                        {
                            InteractionID = Convert.ToInt32(current["InteractionID"].ToString()),
                            FirstInteractor = Convert.ToInt32(current["FirstInteractor".ToString()]),
                            ScndInteractor = Convert.ToInt32(current["ScndInteractor"].ToString()),
                            EffectDescription = Convert.ToString(current["Effect"].ToString()),
                            TypeID = Convert.ToInt32(current["TypeID"].ToString()),
                        });
                }
                return model;
            }
            else
            {
                return model;
            }
        }
        public PharmaciesViewModel GetAllPharmaciesWithPaging(int pageNumber, int pageSize, string sort)
        {
            PharmaciesViewModel model = new PharmaciesViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllPharmaciesWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<PharmacyGeneric>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new PharmacyGeneric()
                        {
                            PharmacyId = Convert.ToInt32(current["PharmacyID"].ToString()),
                            Name = Convert.ToString(current["Name".ToString()]),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            Addressline2 = Convert.ToString(current["AddressLine2"].ToString()),
                            ContactNo = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            LicenseNo = Convert.ToString(current["LicenceNo"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString()),
                            SuburbName = Convert.ToString(current["SuburbName"].ToString()),
                            PharmacistName = Convert.ToString(current["PharmacistName"].ToString()),
                            Image = Convert.ToString(current["Image"].ToString()),
                        });
                }
                return model;
            }
            else
            {
                return model;
            }
        }
        public PharmacistsViewModel GetAllPharmacistsWithPaging(int pageNumber, int pageSize, string sort)
        {
            PharmacistsViewModel model = new PharmacistsViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllPharmacistsWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<PharmacistGeneric>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new PharmacistGeneric()
                        {
                            PharmacistId = Convert.ToInt32(current["PharmacistID"].ToString()),
                            FirstName = Convert.ToString(current["FirstName".ToString()]),
                            LastName = Convert.ToString(current["LastName".ToString()]),
                            ContactNumber = Convert.ToString(current["ContactNo"].ToString()),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            AddressLine2 = Convert.ToString(current["AddressLine2"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            PharmacyId = Convert.ToInt32(current["PharmacyId"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString()),
                            SuburbName = Convert.ToString(current["SuburbName"].ToString()),
                            PharmacyName = Convert.ToString(current["PharmacyName"].ToString()),
                            RegistrationNo = Convert.ToString(current["RegistrationNo"].ToString()),
                        });
                }
                return model;
            }
            else
            {
                return model;
            }
        }
        public MedPracsViewModel GetAllMedPracsWithPaging(int pageNumber, int pageSize, string sort)
        {
            MedPracsViewModel model = new MedPracsViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllMedPracsWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<MedicalPracticeGeneric>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new MedicalPracticeGeneric()
                        {
                            MedPracId = Convert.ToInt32(current["MedPracID"].ToString()),
                            Name = Convert.ToString(current["Name".ToString()]),
                            AddressLine1 = Convert.ToString(current["AddressLine1"].ToString()),
                            AddressLine2 = Convert.ToString(current["AddressLine2"].ToString()),
                            ContactNo = Convert.ToString(current["ContactNo"].ToString()),
                            EmailAddress = Convert.ToString(current["EmailAddress"].ToString()),
                            PracticeNo = Convert.ToString(current["PracticeNo"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString()),
                            SuburbName = Convert.ToString(current["SuburbName"].ToString()),
                            Status = Convert.ToString(current["Status"].ToString()),
                            Image = Convert.ToString(current["Image"].ToString()),
                        });
                }
                return model;
            }
            else
            {
                return model;
            }
        }
        public MedicationViewModel GetAllMedicationWithPaging(int pageNumber, int pageSize, string sort)
        {
            MedicationViewModel model = new MedicationViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllMedicationWithPaging", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                model.DataList = new List<MedicationGeneric>();
                model.OverallCount = ds.Tables.Count;
                model.CurrentPage = pageNumber;
                model.Sort = sort;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    model.DataList.Add(
                        new MedicationGeneric()
                        {
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            ScheduleID = Convert.ToInt32(current["ScheduleID"].ToString()),
                            FormName = Convert.ToString(current["FormName"].ToString()),
                            ScheduleNo = Convert.ToInt32(current["ScheduleNo"].ToString()),
                        });
                }
            }
            return model;
        }
        public Med_IngreGeneric GetMedIngreWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetMedIngreWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Med_IngreGeneric med = new Med_IngreGeneric();
            if (dt.Rows.Count > 0)
            {
                med.MedIngreID = Convert.ToInt32(dt.Rows[0]["MedIngreID"].ToString());
                med.MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"].ToString());
                med.ActiveIngredientID = Convert.ToInt32(dt.Rows[0]["ActiveIngredientID"].ToString());
                med.ActiveStrength = Convert.ToInt32(dt.Rows[0]["ActiveStrength"].ToString());
                med.Description = Convert.ToString(dt.Rows[0]["Description"].ToString());
                med.Medication = GetMedicationWithId(med.MedicationID);
                med.Ingredient = GetActIngreWithId(med.ActiveIngredientID);
            }
            return med;
        }
        public Medication GetMedicationWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetMedicationWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Medication med = new Medication();
            if (dt.Rows.Count > 0)
            {
                med.MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"].ToString());
                med.Name = Convert.ToString(dt.Rows[0]["Name"].ToString());
                med.ScheduleID = Convert.ToInt32(dt.Rows[0]["ScheduleID"].ToString());
            }
            return med;
        }
        public Medication GetMedicationAndIngredientsWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetMedicationWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Medication med = new Medication();
            if (dt.Rows.Count > 0)
            {
                med.MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"].ToString());
                med.Name = Convert.ToString(dt.Rows[0]["Name"].ToString());
                med.DosageID = Convert.ToInt32(dt.Rows[0]["DosageID"].ToString());
                med.ScheduleID = Convert.ToInt32(dt.Rows[0]["ScheduleID"].ToString());
                med.Ingredients = GetAllMedicationIngredient()
                    .FindAll(x => x.MedicationID == med.MedicationID);
            }
            return med;
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
        public PatientDataModel GetPatientAndUserWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPatientAndUserWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PatientDataModel model = new PatientDataModel();
            if (dt.Rows.Count > 0)
            {
                model.User.PatientId = Convert.ToInt32(dt.Rows[0]["PatientID"].ToString());
                model.User.FirstName = Convert.ToString(dt.Rows[0]["FirstName"].ToString());
                model.User.LastName = Convert.ToString(dt.Rows[0]["LastName"].ToString());
                model.User.IdNumber = Convert.ToString(dt.Rows[0]["IdNumber"].ToString());
                model.User.ContactNumber = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                model.User.EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString());
                model.User.AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString());
                model.User.AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString());
                model.User.Gender = Convert.ToString(dt.Rows[0]["Gender".ToString()]);
                model.UserDetails.UserId = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
                model.UserDetails.Username = Convert.ToString(dt.Rows[0]["Username"].ToString());
                model.UserDetails.Password = Convert.ToString(dt.Rows[0]["Password"].ToString());
                model.UserDetails.Role = Convert.ToString(dt.Rows[0]["Role"].ToString());
            }
            return model;
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
                            AddressLine2 = Convert.ToString(current["AddressLine2"].ToString()),
                            SuburbId = Convert.ToInt32(current["SuburbID"].ToString())
                        });
                }
            }
            return Practices;
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
                };
            }
            else
                return new ActiveIngredient();
        }
        public MedicationInteraction GetInteractionWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetInteractionWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new MedicationInteraction()
                {
                    InteractionID = Convert.ToInt32(dt.Rows[0]["InteractionID"].ToString()),
                    FirstInteractor = Convert.ToInt32(dt.Rows[0]["FirstInteractor"].ToString()),
                    ScndInteractor = Convert.ToInt32(dt.Rows[0]["ScndInteractor"].ToString()),
                    EffectDescription = Convert.ToString(dt.Rows[0]["Effect"].ToString()),
                };
            }
            else
                return new MedicationInteraction();
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
        public DoctorUserGeneric GetDoctorWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetDoctorWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            DoctorUserGeneric user = new DoctorUserGeneric();
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
                user.ProvinceID = Convert.ToInt32(dt.Rows[0]["ProvID"].ToString());
                user.CityID = Convert.ToInt32(dt.Rows[0]["CityID"].ToString());
            }
            return user;
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
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool AddPharmacy(Pharmacy model)
        {
            connection();
            dbCmd = new SqlCommand("AddPharmacy", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Name", model.Name);
            dbCmd.Parameters.AddWithValue("@ContactNo", model.ContactNo);
            dbCmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
            dbCmd.Parameters.AddWithValue("@LicenseNo", model.LicenseNo);
            dbCmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
            dbCmd.Parameters.AddWithValue("@AddressLine2", model.Addressline2 ?? "");
            dbCmd.Parameters.AddWithValue("@SuburbID", model.SuburbId);
            dbCmd.Parameters.AddWithValue("@PharmacistID", model.PharmacistID);
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
        public bool AddMedication(Medication medication)
        {
            connection();

            dbCmd = new SqlCommand("AddMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Name", medication.Name);
            dbCmd.Parameters.AddWithValue("@DosageID", medication.DosageID);
            dbCmd.Parameters.AddWithValue("@ScheduleID", medication.ScheduleID);
            dbCmd.Parameters.AddWithValue("@Type", medication.TypeID);
            conn.Open();
            //int i = dbCmd.ExecuteNonQuery();
            int i = Convert.ToInt32(dbCmd.ExecuteScalar().ToString());
            conn.Close();
            if (i >= 1)
            {
                //The stored procedure will return the id of the newly created medication
                //Otherwise it will return 0 if the stored procedure fails
                var result = AddMedicationIngredients(i,medication.Ingredients);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }
        public bool AddMedicationIngredients(int MedId,List<Medication_Ingredient> model)
        {
            bool added = false;
            foreach (Medication_Ingredient m in model)
            {
                connection();
                dbCmd = new SqlCommand("AddMedicationIngredient", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.Parameters.AddWithValue("@MedicationID", MedId);
                dbCmd.Parameters.AddWithValue("@ActiveIngredientID", m.ActiveIngredientID);
                dbCmd.Parameters.AddWithValue("@ActiveStrength", m.ActiveStrength);
                conn.Open();
                int i = dbCmd.ExecuteNonQuery();
                conn.Close();
                if (i >= 1)
                {
                    added = true;
                }
                else
                {
                    added = false;
                    break;
                }
            }
            return added;
        }
        public bool AddInteraction(MedicationInteraction indication)
        {
            connection();

            dbCmd = new SqlCommand("AddInteraction", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@FirstInteractor", indication.FirstInteractor);
            dbCmd.Parameters.AddWithValue("@ScndInteractor", indication.ScndInteractor);
            dbCmd.Parameters.AddWithValue("@Effect", indication.EffectDescription);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool AddICDCode(ICD_Code model)
        {
            connection();

            dbCmd = new SqlCommand("AddICDCode", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Code", model.Code);
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
        public bool AddMedicalPractice(MedicalPractice model)
        {
            connection();
            dbCmd = new SqlCommand("AddMedicalPractice", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Name", model.Name);
            dbCmd.Parameters.AddWithValue("@ContactNo", model.ContactNo);
            dbCmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
            dbCmd.Parameters.AddWithValue("@PracticeNo", model.PracticeNo);
            dbCmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
            dbCmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2??"");
            dbCmd.Parameters.AddWithValue("@SuburbID", model.SuburbId);
            dbCmd.Parameters.AddWithValue("@Status", model.Status);
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
        public bool UpdateInteraction(MedicationInteraction model)
        {
            connection();

            dbCmd = new SqlCommand("UpdateInteraction", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@InteractionID", model.InteractionID);
            dbCmd.Parameters.AddWithValue("@FirstInteractor", model.FirstInteractor);
            dbCmd.Parameters.AddWithValue("@ScndInteractor", model.ScndInteractor);
            dbCmd.Parameters.AddWithValue("@Effect", model.EffectDescription);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool UpdateICDCode(ICD_Code model)
        {
            connection();

            dbCmd = new SqlCommand("UpdateICDCode", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Code", model.Code);
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
        public bool UpdateMedication(Medication model)
        {
            connection();
            dbCmd = new SqlCommand("UpdateMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@MedicationID", model.MedicationID);
            dbCmd.Parameters.AddWithValue("@Name", model.Name);
            dbCmd.Parameters.AddWithValue("@DosageID", model.DosageID);
            dbCmd.Parameters.AddWithValue("@ScheduleID", model.ScheduleID);
            dbCmd.Parameters.AddWithValue("@Type", model.TypeID);
            conn.Open();
            //int i = dbCmd.ExecuteNonQuery();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                //The stored procedure will return the id of the newly created medication
                //Otherwise it will return 0 if the stored procedure fails
                var result = UpdateMedicationIngredient(model.MedicationID, model.Ingredients);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }
        public bool UpdateMedicationIngredient(int medId, List<Medication_Ingredient> model)
        {
            bool added = false;
            foreach (Medication_Ingredient m in model)
            {
                if (m.State == "Old")
                {
                    //Remove the relationship
                    connection();
                    dbCmd = new SqlCommand("RemoveMedicationIngredient", conn);
                    dbCmd.CommandType = CommandType.StoredProcedure;
                    dbCmd.Parameters.AddWithValue("@MedicationID", medId);
                    dbCmd.Parameters.AddWithValue("@ActiveIngredientID", m.ActiveIngredientID);
                    conn.Open();
                    int i = dbCmd.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        added = true;
                    }
                    //add the ralationship again
                    connection();
                    dbCmd = new SqlCommand("AddMedicationIngredient", conn);
                    dbCmd.CommandType = CommandType.StoredProcedure;
                    dbCmd.Parameters.AddWithValue("@MedicationID", medId);
                    dbCmd.Parameters.AddWithValue("@ActiveIngredientID", m.ActiveIngredientID);
                    dbCmd.Parameters.AddWithValue("@ActiveStrength", m.ActiveStrength);
                    conn.Open();
                    i = 0;
                    i = dbCmd.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        added = true;
                    }
                    else
                    {
                        added = false;
                        break;
                    }
                }
                else if(String.IsNullOrEmpty(m.State))
                {
                    connection();
                    dbCmd = new SqlCommand("AddMedicationIngredient", conn);
                    dbCmd.CommandType = CommandType.StoredProcedure;
                    dbCmd.Parameters.AddWithValue("@MedicationID", medId);
                    dbCmd.Parameters.AddWithValue("@ActiveIngredientID", m.ActiveIngredientID);
                    dbCmd.Parameters.AddWithValue("@ActiveStrength", m.ActiveStrength);
                    conn.Open();
                    int i = dbCmd.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        added = true;
                    }
                    else
                    {
                        added = false;
                        break;
                    }
                }
            }
            return added;
        }
        public bool UpdatePharmacy(Pharmacy model)
        {
            connection();

            dbCmd = new SqlCommand("UpdatePharmacy", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PharmacyID", model.PharmacyId);
            dbCmd.Parameters.AddWithValue("@Name", model.Name);
            dbCmd.Parameters.AddWithValue("@ContactNo", model.ContactNo);
            dbCmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
            dbCmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
            dbCmd.Parameters.AddWithValue("@AddressLine2", model.Addressline2??"");
            dbCmd.Parameters.AddWithValue("@LicenseNo", model.LicenseNo);
            dbCmd.Parameters.AddWithValue("@SuburbID", model.SuburbId);
            dbCmd.Parameters.AddWithValue("@PharmacistID", model.PharmacistID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool UpdateDoctor(AddDoctorViewModel model)
        {
            connection();
            DoctorUser doctor = model.User;
            dbCmd = new SqlCommand("UpdateDoctor", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            //Patient table columns
            dbCmd.Parameters.AddWithValue("@DoctorID", doctor.DoctorId);
            dbCmd.Parameters.AddWithValue("@FirstName", doctor.FirstName);
            dbCmd.Parameters.AddWithValue("@LastName", doctor.LastName);
            dbCmd.Parameters.AddWithValue("@EmailAddress", doctor.EmailAddress);
            dbCmd.Parameters.AddWithValue("@ContactNo", doctor.ContactNumber);
            dbCmd.Parameters.AddWithValue("@AddressLine1", doctor.AddressLine1);
            dbCmd.Parameters.AddWithValue("@AddressLine2", doctor.AddressLine2 ?? "");
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
            return false;
        }
        public bool UpdatePharmacist(PharmacistDataModel model)
        {
            connection();
            PharmacistUser pharmacist = model.User;
            var emailExist = CheckEmail(pharmacist.EmailAddress);
            if (!emailExist)
            {
                dbCmd = new SqlCommand("UpdatePharmacist", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                //Patient table columns
                dbCmd.Parameters.AddWithValue("@PharmacistID", pharmacist.PharmacistId);
                dbCmd.Parameters.AddWithValue("@FirstName", pharmacist.FirstName);
                dbCmd.Parameters.AddWithValue("@LastName", pharmacist.LastName);
                dbCmd.Parameters.AddWithValue("@EmailAddress", pharmacist.EmailAddress);
                dbCmd.Parameters.AddWithValue("@ContactNo", pharmacist.ContactNumber);
                dbCmd.Parameters.AddWithValue("@AddressLine1", pharmacist.AddressLine1);
                dbCmd.Parameters.AddWithValue("@AddressLine2", pharmacist.AddressLine2 ?? "");
                dbCmd.Parameters.AddWithValue("@SuburbID", pharmacist.SuburbId);
                dbCmd.Parameters.AddWithValue("@PharmacyID", pharmacist.PharmacyId);
                //User table columns
                dbCmd.Parameters.AddWithValue("@UserID", model.UserDetails.UserId);
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
        public bool UpdateMedicalPractice(MedicalPractice model)
        {
            connection();

            dbCmd = new SqlCommand("UpdateMedicalPractice", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@MedPracID", model.MedPracId);
            dbCmd.Parameters.AddWithValue("@Name", model.Name);
            dbCmd.Parameters.AddWithValue("@ContactNo", model.ContactNo);
            dbCmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
            dbCmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
            dbCmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2 ?? "");
            dbCmd.Parameters.AddWithValue("@PracticeNo", model.PracticeNo);
            dbCmd.Parameters.AddWithValue("@SuburbID", model.SuburbId);
            dbCmd.Parameters.AddWithValue("@Status", model.Status);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool UpdatePatient(PatientDataModel patientModel)
        {
            connection();
            PatientUser model = patientModel.User;

            dbCmd = new SqlCommand("UpdatePatient", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            //Patient table columns
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientId);
            dbCmd.Parameters.AddWithValue("@FirstName", model.FirstName);
            dbCmd.Parameters.AddWithValue("@LastName", model.LastName);
            dbCmd.Parameters.AddWithValue("@IdNumber", model.IdNumber);
            dbCmd.Parameters.AddWithValue("@Gender", model.Gender);
            dbCmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
            dbCmd.Parameters.AddWithValue("@ContactNo", model.ContactNumber);
            dbCmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
            dbCmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2 ?? "");
            dbCmd.Parameters.AddWithValue("@SuburbID", model.SuburbID);
            //User table columns
            dbCmd.Parameters.AddWithValue("@UserID", patientModel.UserDetails.UserId);
            dbCmd.Parameters.AddWithValue("@Username", model.EmailAddress);
            dbCmd.Parameters.AddWithValue("@Password", patientModel.UserDetails.Password);
            dbCmd.Parameters.AddWithValue("@Role", patientModel.UserDetails.Role);
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
                dbCmd.Parameters.AddWithValue("@AddressLine2", doctor.AddressLine2 ?? "");
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
        public bool AddPatient(PatientDataModel patientModel)
        {
            connection();
            PatientUser model = patientModel.User;
            var emailExist = CheckEmail(model.EmailAddress);
            if (!emailExist)
            {
                dbCmd = new SqlCommand("AddPatient", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                //Patient table columns
                dbCmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                dbCmd.Parameters.AddWithValue("@LastName", model.LastName);
                dbCmd.Parameters.AddWithValue("@IdNumber", model.IdNumber);
                dbCmd.Parameters.AddWithValue("@Gender", model.Gender);
                dbCmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                dbCmd.Parameters.AddWithValue("@ContactNo", model.ContactNumber);
                dbCmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
                dbCmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2 ?? "");
                dbCmd.Parameters.AddWithValue("@SuburbID", model.SuburbID);
                //User table columns
                dbCmd.Parameters.AddWithValue("@Username", model.EmailAddress);
                dbCmd.Parameters.AddWithValue("@Password", patientModel.UserDetails.Password);
                dbCmd.Parameters.AddWithValue("@Role", patientModel.UserDetails.Role);
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
        public bool AddPharmacist(PharmacistDataModel model)
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
        public bool DeleteActiveIngredient(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeleteActiveIngredient", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeletePharmacy(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePharmacy", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeletePharmacist(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePharmacist", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeletePatient(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePatient", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeleteInteraction(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeleteInteraction", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeleteMedication(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeleteMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeleteMedPrac(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeleteMedPrac", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeleteICDCode(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeleteICDCode", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeleteDoctor(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeleteDoctor", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
            return false;
        }
        public bool DeleteContraIndication(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeleteContraIndication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
                return true;
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
