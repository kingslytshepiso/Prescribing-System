using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;
using Microsoft.CodeAnalysis.CSharp;
using Prescribing_System.Areas.Pharmacist.Models;
using static Prescribing_System.Areas.Doctor.Models.PatientViewModel;

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
            string constring = "Data Source = sict-sql.mandela.ac.za; Initial Catalog = GRP-4-10-E-Prescribing; Integrated Security = false; User ID = GRP-4-10; Password = grp-4-10-soit2022";
            conn = new SqlConnection(constring);
        }

        public PatientListContex GetAllPatientsWithPaging(int pageNumber, int pageSize)
        {
            PatientListContex patients = new PatientListContex();
            connection();
            dbCmd = new SqlCommand("GetAllPatientWithPaging", conn);
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
                            IdNumber = Convert.ToString(current["IdNumber"].ToString()),
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
        public Prescribing_System.Models.Patient GetPatientWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPatientWithIds", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new Prescribing_System.Models.Patient()
                {
                    PatientID = Convert.ToInt32(dt.Rows[0]["PatientID"].ToString()),
                    FirstName = Convert.ToString(dt.Rows[0]["FirstName"].ToString()),
                    LastName = Convert.ToString(dt.Rows[0]["LastName"].ToString()),
                    EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"].ToString()),
                    ContactNumber = Convert.ToString(dt.Rows[0]["ContactNo"].ToString()),
                    IdNumber = Convert.ToString(dt.Rows[0]["IdNumber"].ToString()),
                    AddressLine1 = Convert.ToString(dt.Rows[0]["AddressLine1"].ToString()),
                    AddressLine2 = Convert.ToString(dt.Rows[0]["AddressLine2"].ToString()),
                    SuburbID = Convert.ToInt32(dt.Rows[0]["SuburbID"].ToString())
                };
            }
            else
                return new Prescribing_System.Models.Patient();
        }
        //public PatientPrescriptionViewModel GetPrescriptionById(int id)
        //{
        //    connection();
        //    dbCmd = new SqlCommand("GetPatientWithIds", conn);
        //    dbCmd.CommandType = CommandType.StoredProcedure;
        //    dbCmd.Parameters.AddWithValue("@id", id);
        //    dt = new DataTable();
        //    dbAdapter = new SqlDataAdapter(dbCmd);
        //    dbAdapter.Fill(dt);
        //    conn.Close();
        //    if (dt.Rows.Count > 0)
        //    {
        //        return new GenericPrescriptionLine()
        //        {
        //            Prescription = Convert.ToInt32(dt.Rows[0]["PrescriptionID"].ToString()),
        //            //Date = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()),
        //        };
        //    }
        //    return new PatientPrescriptionViewModel();
        //}
        public bool AddPrescription(PatientPrescriptionViewModel viewModel,int doctorID, int patientID,string nowDate)
        {
            connection();
            dbCmd = new SqlCommand("AddPrescriptions", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@DoctorID", doctorID);
            dbCmd.Parameters.AddWithValue("@PatientID", patientID);
            dbCmd.Parameters.AddWithValue("@Date", nowDate);
            
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
        public List<Medication> GetAllMeds()
        {
            List<Medication> meds = new List<Medication>();
            connection();
            dbCmd = new SqlCommand("GetAllMeds", conn);
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
                        new Medication()
                        {
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            ScheduleID = Convert.ToInt32(current["ScheduleID"].ToString()),
                        });
                }
            }
            return meds;
        }
        public List<Medication> GetAcuteMedications()
        {
            List<Medication> meds = new List<Medication>();
            connection();
            dbCmd = new SqlCommand("GetAcuteMedications", conn);
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
                        new Medication()
                        {
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                             DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            ScheduleID = Convert.ToInt32(current["ScheduleID"].ToString()),
                        });
                }
            }
            return meds;
        }
        public List<Medication> GetChonicMedications()
        {
            List<Medication> meds = new List<Medication>();
            connection();
            dbCmd = new SqlCommand("GetChonicMedications", conn);
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
                        new Medication()
                        {
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            ScheduleID = Convert.ToInt32(current["ScheduleID"].ToString()),
                        });
                }
            }
            return meds;
        }
        public List<Medication> GetAllChronicMeds()
        {
            List<Medication> meds = new List<Medication>();
            connection();
            dbCmd = new SqlCommand("GetAllChronicMeds", conn);
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
                        new Medication()
                        {
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            // DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            ScheduleID = Convert.ToInt32(current["ScheduleID"].ToString()),
                        });
                }
            }
            return meds;
        }
        public List<MedicationIngredient> GetAllMedicationIngredients()
        {
            List<MedicationIngredient> meds = new List<MedicationIngredient>();
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
                        new MedicationIngredient()
                        {
                            MedIngreID = Convert.ToInt32(current["MedIngreID"].ToString()),
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngredientID"].ToString()),
                            MedicationName = Convert.ToString(current["MedicationName"].ToString()),
                            IngredientName = Convert.ToString(current["IngredientName"].ToString()),
                            // DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            ActiveStrength = Convert.ToDouble(current["ActiveStrength"].ToString()),
                        });
                }
            }
            return meds;
        }
        public List<ActiveIngredient> GetAllActiveIngredients()
        {
            List<ActiveIngredient> ingredients = new List<ActiveIngredient>();
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
                    ingredients.Add(
                        new ActiveIngredient()
                        {
                            ActiveIngreID = Convert.ToInt32(current["ActiveIngreID"].ToString()),
                            Name = Convert.ToString(current["Name"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                        });
                }
            }
            return ingredients;
        }
        public bool AddPrescriptionLine(PatientPrescriptionViewModel model)
        {
            connection();
            dbCmd = new SqlCommand("AddPrescriptionLines", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@MedicationID", model.Medication.MedicationID);
            dbCmd.Parameters.AddWithValue("@Quantity", model.PrescriptionLine.Quantity);
            dbCmd.Parameters.AddWithValue("@DosageID", model.Dosage.DosageID);
            dbCmd.Parameters.AddWithValue("@RepeatNo", model.PrescriptionLine.RepeatNo);
            dbCmd.Parameters.AddWithValue("@RepeatLeftNo", model.PrescriptionLine.RepeatLeftNo);
            dbCmd.Parameters.AddWithValue("@Status", model.PrescriptionLine.Status);
            dbCmd.Parameters.AddWithValue("@Instruction", model.PrescriptionLine.Instruction);

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
        public PatientPrescriptionViewModel GetAllPresciptionLines(int patientId, int doctorId)
        {
            PatientPrescriptionViewModel Lines = new PatientPrescriptionViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllPresciptionLines", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientId);
            dbCmd.Parameters.AddWithValue("@DoctorID", doctorId);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    Lines.DataList.Add(
                        new GenericPrescriptionLine()
                        {
                            Line = new PrescriptionLine()
                            {
                                PresciptionLineID = Convert.ToInt32(row["LineID"].ToString()),
                                DosageID = Convert.ToInt32(row["DosageID"].ToString()),
                                MedicationID = Convert.ToInt32(row["MedicationID"].ToString()),
                                Quantity = Convert.ToString(row["Quantity"].ToString()),
                                Instruction = Convert.ToString(row["Instruction"].ToString()),
                                PrescriptionID = Convert.ToInt32(row["PrescriptionID"].ToString()),
                                RepeatNo = Convert.ToInt32(row["RepeatNo"].ToString()),
                                RepeatLeftNo = Convert.ToInt32(row["RepeatLeftNo"].ToString()),
                                Status = Convert.ToString(row["Status"].ToString()),
                                //extra col
                            },
                            Prescription = new Prescription()
                            {
                                PrescriptionID = Convert.ToInt32(row["PrescriptionID"].ToString()),
                                PrescriptionDate = Convert.ToString(row["Date"].ToString()),
                                PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                                DoctorID = Convert.ToInt32(row["DoctorID"].ToString()),
                            },
                            Patient = new Admin.Models.System_Users.PatientUser()
                            {
                                PatientId = Convert.ToInt32(row["PatientID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                            },
                            Doctor = new Doctor()
                            {
                                DoctorId = Convert.ToInt32(row["DoctorID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                                MedPracId = Convert.ToInt32(row["MedPracID"].ToString()),
                                HighestQual = Convert.ToString(row["HighestQualification"].ToString()),
                            },
                            Dosage = new Dosage()
                            {
                                DosageID = Convert.ToInt32(row["DosageID"].ToString()),
                                FormName = Convert.ToString(row["FormName"].ToString()),
                            },
                            Medication = new Medication()
                            {
                                MedicationID = Convert.ToInt32(row["MedicationID"].ToString()),
                                Name = Convert.ToString(row["Name"].ToString()),
                                DosageID = Convert.ToInt32(row["DosageID"].ToString()),
                                ScheduleID = Convert.ToInt32(row["ScheduleID"].ToString()),
                            },
                            //Practice = new Admin.Models.System_Objects.MedicalPractice()
                            //{
                            //    MedPracId = Convert.ToInt32(row["MedPracId"].ToString()),
                            //    PracticeNo = Convert.ToString(row["PracticeNo"].ToString()),
                            //    Name = Convert.ToString(row["Name"].ToString()),
                            //    ContactNo = Convert.ToString(row["ContactNo"].ToString()),
                            //    EmailAddress = Convert.ToString(row["EmailAddress"].ToString()),
                            //    AddressLine1 = Convert.ToString(row["Addressline1"].ToString()),
                            //    Addressline2 = Convert.ToString(row["Addressline2"].ToString()),
                            //    SuburbId = Convert.ToInt32(row["SuburbId"].ToString()),
                            //}
                        });
                    
                }
               
            }
            return Lines;
        }
        public List<Dosage> GetAllDosage()
        {
            List<Dosage> list = new List<Dosage>();
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
                    list.Add(
                        new Dosage()
                        {
                            DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            FormName = Convert.ToString(current["FormName"].ToString()),
                            
                        });
                }
            }
            return list;
        }
        public List<ChronicDisease> GetAllChronicDiseases()
        {
            List<ChronicDisease> chronics = new List<ChronicDisease>();
            connection();
            dbCmd = new SqlCommand("GetAllChronicDiseases", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    chronics.Add(
                        new ChronicDisease()
                        {
                            ChronicDiseaseID = Convert.ToInt32(row["ChronicDiseaseID"].ToString()),
                            Date = Convert.ToDateTime(row["Date"].ToString()),
                            //PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                            DiseaseID = Convert.ToInt32(row["DiseaseID"].ToString()),
                        });
                }  
            }
            return chronics;
        }
        public List<Disease> GetAllDiseases()
        {
            List<Disease> diseases = new List<Disease>();
            connection();
            dbCmd = new SqlCommand("GetAllDiseases", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    diseases.Add(
                        new Disease()
                        {
                            DiseaseID = Convert.ToInt32(row["DiseaseID"].ToString()),
                            DiseaseName = Convert.ToString(row["Name"].ToString()),
                        });
                }
            }
            return diseases;

        }
        public List<Disease> GetAllChronicDiseasesD()
        {
            List<Disease> diseases = new List<Disease>();
            connection();
            dbCmd = new SqlCommand("GetAllChronicDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    diseases.Add(
                        new Disease()
                        {
                            DiseaseID = Convert.ToInt32(row["DiseaseID"].ToString()),
                            DiseaseName = Convert.ToString(row["Name"].ToString()),
                        });
                }
            }
            return diseases;

        }
        public List<Disease> GetAllAcuteDiseasesD()
        {
            List<Disease> diseases = new List<Disease>();
            connection();
            dbCmd = new SqlCommand("GetAllAcuteDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    diseases.Add(
                        new Disease()
                        {
                            DiseaseID = Convert.ToInt32(row["DiseaseID"].ToString()),
                            DiseaseName = Convert.ToString(row["Name"].ToString()),
                        });
                }
            }
            return diseases;

        }
        public List<Prescribing_System.Models.Patient> GetPatients()
        {
            List<Prescribing_System.Models.Patient> patients = new List<Prescribing_System.Models.Patient>();
            connection();
            dbCmd = new SqlCommand("GetPatients", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    patients.Add(
                        new Prescribing_System.Models.Patient()
                        {
                            PatientID = Convert.ToInt32(row["PatientID"].ToString()),

                        });
                }
            }
            return patients;
        }
        public bool AddPatientChronicDisease(PatientChronicDiseaseModel model)
        {
            connection();
            dbCmd = new SqlCommand("AddPatientChronicDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@DiseaseID", model.ChronicDisease.DiseaseID);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);
            dbCmd.Parameters.AddWithValue("@Date", model.ChronicDisease.Date);


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
        public bool AddPatientAcuteDisease(PatientAcuteDiseaseModel model)
        {
            connection();
            dbCmd = new SqlCommand("AddPatientAcuteDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@DiseaseID", model.AcuteDisease.DiseaseID);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);
            dbCmd.Parameters.AddWithValue("@Date", model.AcuteDisease.Date);

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
        public bool AddPatientChronicMedication(PatientChronicMedicationModel model)
        {
            connection();
            dbCmd = new SqlCommand("AddPatientChronicMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@MedicationID", model.List.Medication.MedicationID);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);

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
        public bool AddPatientAcuteMedication(PatientAcuteMedicationModel model)
        {
            connection();
            dbCmd = new SqlCommand("AddPatientAcuteMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@MedicationID", model.acuteMedication.Medication.MedicationID);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);

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
        public bool AddPatientAllergies(PatientAllergyModel model)
        {
            connection();
            dbCmd = new SqlCommand("AddPatientAllergies", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ActiveIngreID", model.allergy.Allergy.ActiveIngreID);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);

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
        public PatientChronicDiseaseModel GetAllPatientChronicDisease(int patientID, int pageNumber, int pageSize)
        {
            PatientChronicDiseaseModel chronics = new PatientChronicDiseaseModel();
            connection();
            dbCmd = new SqlCommand("GetAllPatientChronicDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientID);
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            // dbCmd.Parameters.AddWithValue("@DoctorID", doctorID);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                chronics.chronicDiseaseModels = new List<ListPatientChronicDisease>();
                chronics.OverallCount = ds.Tables.Count;
                chronics.CurrentPage = pageNumber;
                chronics.PatientID = patientID;
                foreach (DataRow row in ds.Tables[pageNumber - 1].Rows)
                {
                    chronics.chronicDiseaseModels.Add(
                        new ListPatientChronicDisease()
                        {
                            Patient = new Prescribing_System.Models.Patient()
                            {
                                PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                            },
                            ChronicDisease = new ChronicDisease()
                            {
                                ChronicDiseaseID = Convert.ToInt32(row["PatientDiseaseID"].ToString()),
                                Date = Convert.ToDateTime(row["DiagnosisDate"].ToString()),

                            },
                            Disease = new Disease()
                            {
                                DiseaseID = Convert.ToInt32(row["DiseaseID"].ToString()),
                                DiseaseName = Convert.ToString(row["Name"].ToString()),
                            },
                            //Doctor = new Doctor()
                            //{
                            //    DoctorId = Convert.ToInt32(row["DoctorID"].ToString()),
                            //    FirstName = Convert.ToString(row["FirstName"].ToString()),
                            //    LastName = Convert.ToString(row["LastName"].ToString()),
                            //    MedPracId = Convert.ToInt32(row["MedPracID"].ToString()),
                            //    HighestQual = Convert.ToString(row["HighestQualification"].ToString()),
                            //}

                        });
                }
                return chronics;
            }
            return chronics;
        }
        public PatientAcuteDiseaseModel GetAllPatientAcuteDisease(int patientID, int pageNumber, int pageSize)
        {
            PatientAcuteDiseaseModel acutes = new PatientAcuteDiseaseModel();
            connection();
            dbCmd = new SqlCommand("GetAllPatientAcuteDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientID);
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            // dbCmd.Parameters.AddWithValue("@DoctorID", doctorID);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                acutes.lists = new List<ListPatientAcuteDisease>();
                acutes.OverallCount = ds.Tables.Count;
                acutes.CurrentPage = pageNumber;
                acutes.PatientID = patientID;
                foreach (DataRow row in ds.Tables[pageNumber - 1].Rows)
                {
                    acutes.lists.Add(
                        new ListPatientAcuteDisease()
                        {
                            Patient = new Prescribing_System.Models.Patient()
                            {
                                PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                            },
                            Disease = new Disease()
                            {
                                DiseaseID = Convert.ToInt32(row["DiseaseID"].ToString()),
                                DiseaseName = Convert.ToString(row["Name"].ToString()),
                            },
                            AcuteDisease = new AcuteDisease()
                            {
                                AcuteDiseaseID = Convert.ToInt32(row["PatientDiseaseID"].ToString()),
                                Date  = Convert.ToString(row["DiagnosisDate"].ToString()),
                                //PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                            },
                        });
                    
                }
                return acutes;
            }
            else
            {
                return acutes;
            }
            
        }
        public PatientChronicMedicationModel GetAllPatientChronicMedications(int patientID, int pageNumber, int pageSize)
        {
            PatientChronicMedicationModel chronicMedicationModel = new PatientChronicMedicationModel();
            connection();
            dbCmd = new SqlCommand("GetAllPatientChronicMedications", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientID);
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            // dbCmd.Parameters.AddWithValue("@DoctorID", doctorID);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                chronicMedicationModel.chronicMedications = new List<ListPatientChronicMedication>();
                chronicMedicationModel.OverallCount = ds.Tables.Count;
                chronicMedicationModel.CurrentPage = pageNumber;
                chronicMedicationModel.PatientID = patientID;
                foreach (DataRow row in ds.Tables[pageNumber - 1].Rows)
                {
                    chronicMedicationModel.chronicMedications.Add(
                        new ListPatientChronicMedication()
                        {
                            Patient = new Prescribing_System.Models.Patient()
                            {
                                PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                            },
                            Medication = new Medication()
                            {
                                MedicationID = Convert.ToInt32(row["MedicationID"].ToString()),
                                Name = Convert.ToString(row["Name"].ToString()),
                                DosageID = Convert.ToInt32(row["DosageID"].ToString()),
                                ScheduleID = Convert.ToInt32(row["ScheduleID"].ToString()),
                                ActiveIngredientID = Convert.ToInt32(row["ActiveIngredientID"].ToString()),
                            },
                            ChronicMedication = new ChronicMedication()
                            {
                                ChronicMedID = Convert.ToInt32(row["PatientMedID"].ToString()),
                                MedIngreID = Convert.ToInt32(row["MedIngreID"].ToString()),
                            },
                            
                            
                        });
                    
                }
                return chronicMedicationModel;

            }
            else
                return chronicMedicationModel;

            
        }
        public PatientAcuteMedicationModel GetAllPatientAcuteMedications(int patientID, int pageNumber, int pageSize)
        {
            PatientAcuteMedicationModel acuteMedicationModel = new PatientAcuteMedicationModel();
            connection();
            dbCmd = new SqlCommand("GetAllPatientAcuteMedications", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientID);
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            // dbCmd.Parameters.AddWithValue("@DoctorID", doctorID);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                acuteMedicationModel.lists = new List<ListPatientAcuteMedication>();
                acuteMedicationModel.OverallCount = ds.Tables.Count;
                acuteMedicationModel.CurrentPage = pageNumber;
                acuteMedicationModel.PatientID = patientID;
                foreach (DataRow row in ds.Tables[pageNumber - 1].Rows)
                {
                    acuteMedicationModel.lists.Add(
                        new ListPatientAcuteMedication()
                        {
                            Patient = new Prescribing_System.Models.Patient()
                            {
                                PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                            },
                            Medication = new Medication()
                            {
                                MedicationID = Convert.ToInt32(row["MedicationID"].ToString()),
                                Name = Convert.ToString(row["Name"].ToString()),
                                DosageID = Convert.ToInt32(row["DosageID"].ToString()),
                                ScheduleID = Convert.ToInt32(row["ScheduleID"].ToString()),
                            },
                            AcuteMedication = new AcuteMedication()
                            {
                                AcuteMedicationID = Convert.ToInt32(row["PatientMedID"].ToString()),
                                MedicationID = Convert.ToInt32(row["MedIngreID"].ToString()),
                            },
                        });
                    
                }
                return acuteMedicationModel;
            }
            return acuteMedicationModel;
        }
        public PatientAllergyModel GetAllPatientAllergy(int patientID, int pageNumber, int pageSize)
        {
            PatientAllergyModel patientAllergy = new PatientAllergyModel();
            connection();
            dbCmd = new SqlCommand("GetAllPatientAllergy", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientID);
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            // dbCmd.Parameters.AddWithValue("@DoctorID", doctorID);
            ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                patientAllergy.lists = new List<ListPatientAllergy>();
                patientAllergy.OverallCount = ds.Tables.Count;
                patientAllergy.CurrentPage = pageNumber;
                patientAllergy.PatientID = patientID;
                foreach (DataRow row in ds.Tables[pageNumber - 1].Rows)
                {
                    patientAllergy.lists.Add(
                        new ListPatientAllergy()
                        {
                            ActiveIngredient = new ActiveIngredient()
                            {
                                ActiveIngreID = Convert.ToInt32(row["ActiveIngreID"].ToString()),
                                Name = Convert.ToString(row["Name"].ToString()),
                                Description = Convert.ToString(row["Description"].ToString()),
                            },
                            Patient = new Prescribing_System.Models.Patient()
                            {
                                PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                            },
                            Allergy = new Allergy()
                            {
                                AllergyID = Convert.ToInt32(row["AllergyID"].ToString()),
                            }
                        });
                    
                }
                return patientAllergy;
            }
            return patientAllergy;
        }
        public List<CurrentDoctorVisit> GetAllCurrentDoctor(int patientID)
        {
            List<CurrentDoctorVisit> currentDoctorVisits = new List<CurrentDoctorVisit>();
            connection();
            dbCmd = new SqlCommand("GetAllCurrentDoctor", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientID);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    currentDoctorVisits.Add(
                        new CurrentDoctorVisit()
                        {
                            VistID = Convert.ToInt32(row["VistID"].ToString()),
                            ReasonOfVist = Convert.ToString(row["ReasonOfVist"].ToString()),
                            Whathurts = Convert.ToString(row["Whathurts"].ToString()),
                            Symptoms = Convert.ToString(row["Symptoms"].ToString()),
                            SymptomDurtion = Convert.ToString(row["SymptomDurtion"].ToString()),
                            DoctorID = Convert.ToInt32(row["DoctorID"].ToString()),
                            PatientID = Convert.ToInt32(row["PatientID"].ToString()),
                        });
                    
                }
                return currentDoctorVisits;
            }
            return currentDoctorVisits;
        }
        public bool AddCurrentDoctorVist(CurrentDoctorVisit model)
        {
            connection();
            dbCmd = new SqlCommand("AddCurrentDoctorVist", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ReasonOfVist", model.ReasonOfVist);
            dbCmd.Parameters.AddWithValue("@Whathurts", model.Whathurts);
            dbCmd.Parameters.AddWithValue("@Symptoms", model.Symptoms);
            dbCmd.Parameters.AddWithValue("@SymptomDurtion", model.SymptomDurtion);
            dbCmd.Parameters.AddWithValue("@Date", model.VisitDate);
            dbCmd.Parameters.AddWithValue("@PatientID", model.PatientID);
            dbCmd.Parameters.AddWithValue("@DoctorID", model.DoctorID);

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
        public ChronicDisease GetAllChronicDiesase(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetAllChronicDiesaseByID", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new ChronicDisease()
                {
                    ChronicDiseaseID = Convert.ToInt32(dt.Rows[0]["ChronicMedHistoryID"].ToString()),
                    DiseaseID = Convert.ToInt32(dt.Rows[0]["DiseaseID"].ToString()),
                    Date = Convert.ToDateTime(dt.Rows[0]["Date"]),

                };
            }
            return new ChronicDisease();
        }
        public bool UpdatePatientChronicDisease(ChronicDisease model)
        {
            connection();
            dbCmd = new SqlCommand("UpdatePatientChronicDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientDiseaseID", model.ChronicDiseaseID);
            dbCmd.Parameters.AddWithValue("@Date", model.Date);
            dbCmd.Parameters.AddWithValue("@DiseaseID", model.DiseaseID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool UpdatePatientAcuteDisease(AcuteDisease model)
        {
            connection();
            dbCmd = new SqlCommand("UpdatePatientAcuteDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@AcuteDiseaseID", model.AcuteDiseaseID);
            dbCmd.Parameters.AddWithValue("@Date", model.Date);
            dbCmd.Parameters.AddWithValue("@DiseaseID", model.DiseaseID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool UpdatePatientChronicMedication(ChronicMedication model)
        {
            connection();
            dbCmd = new SqlCommand("UpdatePatientChronicMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ChronicMedID", model.ChronicMedID);
            dbCmd.Parameters.AddWithValue("@MedicationID", model.MedicationID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool UpdatePatientAcuteMedication(AcuteMedication model)
        {
            connection();
            dbCmd = new SqlCommand("UpdatePatientAcuteMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@AcuteMedicationID", model.AcuteMedicationID);
            dbCmd.Parameters.AddWithValue("@MedicationID", model.MedicationID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool UpdatePatientAllergy(Allergy model)
        {
            connection();
            dbCmd = new SqlCommand("UpdatePatientAllergy", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@AllergyID", model.AllergyID);
            dbCmd.Parameters.AddWithValue("@ActiveIngreID", model.ActiveIngreID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletePatientChronicDisease(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePatientChronicDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletePatientAcuteDisease(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePatientAcuteDisease", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@AcuteDiseaseID", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletePatientChronicMedication(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePatientChronicMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ChronicMedID", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletePatientAcuteMedication(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePatientAcuteMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@AcuteMedicationID", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletePatientAllergy(int id)
        {
            connection();
            dbCmd = new SqlCommand("DeletePatientAllergy", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@AllergyID", id);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletePatientPrescritionLine(PrescriptionLine model)
        {
            connection();
            dbCmd = new SqlCommand("DeletePatientPrescritionLine", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@LineID", model.PresciptionLineID);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public AcuteDisease GetAcuteDiseaseById(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetAllPatientAcuteDiesaseById", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new AcuteDisease()
                {
                    AcuteDiseaseID = Convert.ToInt32(dt.Rows[0]["AcuteDiseaseID"].ToString()),
                    DiseaseID = Convert.ToInt32(dt.Rows[0]["DiseaseID"].ToString()),
                    Date = Convert.ToString(dt.Rows[0]["Date"]),

                };
            }
            return new AcuteDisease();
        }
        public ChronicMedication GetChronicMedicationById(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetAllPatientChronicMedicationById", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new ChronicMedication()
                {
                    ChronicMedID = Convert.ToInt32(dt.Rows[0]["ChronicMedHistoryID"].ToString()),
                    MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"].ToString()),

                };
            }
            return new ChronicMedication();
        }
        public AcuteMedication GetAcuteMedicationById(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetAllPatientAcuteMedicationById", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new AcuteMedication()
                {
                    AcuteMedicationID = Convert.ToInt32(dt.Rows[0]["AcuteMedicationID"].ToString()),
                    MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"].ToString()),

                };
            }
            return new AcuteMedication();
        }
        public Allergy GetAllergyById(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetAllPatientAllergyById", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new Allergy()
                {
                    AllergyID = Convert.ToInt32(dt.Rows[0]["AllergyID"].ToString()),
                    ActiveIngreID = Convert.ToInt32(dt.Rows[0]["ActiveIngreID"].ToString()),

                };
            }
            return new Allergy();
        }
        public PrescriptionLine GetPrescriptionLineById(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetAllPatientPrescriptionLineById", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new PrescriptionLine()
                {
                    PresciptionLineID = Convert.ToInt32(dt.Rows[0]["LineID"].ToString()),
                    PrescriptionID = Convert.ToInt32(dt.Rows[0]["PrescriptionID"].ToString()),
                    MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"]),

                };
            }
            return new PrescriptionLine();
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
        public List<Prescription> GetPrescriptionsWithPatientId(int id)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var p = GetPrescriptionWithPatientId(id, i);
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
        public Prescription GetPrescriptionWithPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetPrescriptionWithPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Prescription prescription = null;
            if (dt.Rows.Count > 0)
            {
                if (i < dt.Rows.Count)
                {
                    prescription = new Prescription();
                    prescription.PrescriptionID = Convert.ToInt32(dt.Rows[i]["PrescriptionID"].ToString());
                    prescription.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                    prescription.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    prescription.DoctorID = Convert.ToInt32(dt.Rows[i]["DoctorID"].ToString());
                }
            }
            return prescription;
        }
        public List<PrescriptionLine> GetPrescLinesWithPrescId(int id)
        {
            List<PrescriptionLine> lines = new List<PrescriptionLine>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var p = GetPrescLineWithPrescId(id, i);
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
        public PrescriptionLine GetPrescLineWithPrescId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetPrescLineWithPrescId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PrescriptionLine line = null;
            if (dt.Rows.Count > 0)
            {
                if (i < dt.Rows.Count)
                {
                    line = new PrescriptionLine();
                    line.PresciptionLineID = Convert.ToInt32(dt.Rows[i]["LineID"].ToString());
                    line.Quantity = Convert.ToString(dt.Rows[i]["Quantity"].ToString());
                    line.Instruction = Convert.ToString(dt.Rows[i]["Instruction"].ToString());
                    line.RepeatNo = Convert.ToInt32(dt.Rows[i]["RepeatNo"].ToString());
                    line.RepeatLeftNo = Convert.ToInt32(dt.Rows[i]["RepeatLeftNo"].ToString());
                    line.Status = Convert.ToString(dt.Rows[i]["Status"].ToString());
                    line.PrescriptionID = Convert.ToInt32(dt.Rows[i]["PrescriptionID"].ToString());
                    line.MedicationID = Convert.ToInt32(dt.Rows[i]["MedicationID"].ToString());
                }
            }
            return line;
        }
        public Doctor GetDoctorWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetDoctorWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Doctor user = new Doctor();
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
        public ChronicDisease GetChronicDiseaseByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetChronicDiseaseByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            ChronicDisease chronicDisease = null;
            if(dt.Rows.Count > 0)
            {
                if (i < dt.Rows.Count)
                {
                    chronicDisease = new ChronicDisease();
                    chronicDisease.ChronicDiseaseID = Convert.ToInt32(dt.Rows[i]["PatientDiseaseID"].ToString());
                    chronicDisease.Date = Convert.ToDateTime(dt.Rows[i]["DiagnosisDate"].ToString());
                    chronicDisease.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    chronicDisease.DiseaseID = Convert.ToInt32(dt.Rows[i]["DiseaseID"].ToString());
                }
            }
            return chronicDisease;
        }
        public AcuteDisease GetAcuteDiseaseByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetAcuteDiseaseByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            AcuteDisease acuteDisease = null;
            if(dt.Rows.Count > 0)
            {
                if (i < dt.Rows.Count)
                {
                    acuteDisease = new AcuteDisease();
                    acuteDisease.AcuteDiseaseID = Convert.ToInt32(dt.Rows[i]["PatientDiseaseID"].ToString());
                    acuteDisease.Date = Convert.ToString(dt.Rows[i]["DiagnosisDate"].ToString());
                    acuteDisease.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    acuteDisease.DiseaseID = Convert.ToInt32(dt.Rows[i]["DiseaseID"].ToString());
                }
            }
            return acuteDisease;
        }
        public Allergy GetAllergyByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetAllergyByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            Allergy allergy = null;
            if (dt.Rows.Count > 0)
            {
                if(i < dt.Rows.Count)
                {
                    allergy = new Allergy();
                    allergy.AllergyID = Convert.ToInt32(dt.Rows[i]["AllergyID"].ToString());
                    allergy.ActiveIngreID = Convert.ToInt32(dt.Rows[i]["ActiveIngredientID"].ToString());
                    allergy.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                }
            }
            return allergy;
        }
        public ChronicMedication GetChronicMedicationByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetChronicMedicationByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            ChronicMedication chronic = null;
            if(dt.Rows.Count > 0)
            {
                if (i < dt.Rows.Count)
                {
                    chronic = new ChronicMedication();
                    chronic.ChronicMedID = Convert.ToInt32(dt.Rows[i]["PatientMedID"].ToString());
                    chronic.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    chronic.MedicationID = Convert.ToInt32(dt.Rows[i]["MedicationID"].ToString());
                    chronic.MedIngreID = Convert.ToInt32(dt.Rows[i]["MedIngreID"].ToString());
                    chronic.ActiveIngredientID = Convert.ToInt32(dt.Rows[i]["ActiveIngredientID"].ToString());
                    chronic.DosageID = Convert.ToInt32(dt.Rows[i]["DosageID"].ToString());
                }
            }
            return chronic;
        }
        public AcuteMedication GetAcuteMedicationByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetAcuteMedicationByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            AcuteMedication acute = null;
            if (dt.Rows.Count > 0)
            {
                if(i < dt.Rows.Count)
                {
                    acute = new AcuteMedication();
                    acute.AcuteMedicationID = Convert.ToInt32(dt.Rows[i]["PatientMedID"].ToString());
                    acute.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    acute.MedicationID = Convert.ToInt32(dt.Rows[i]["MedicationID"].ToString());
                }
            }
            return acute;
        }
        public CurrentDoctorVisit GetCurrentDoctorVisitByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetCurrentDoctorVisitByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            CurrentDoctorVisit visit = null;
            if (dt.Rows.Count > 0)
            {
                if(i < dt.Rows.Count)
                {
                    visit = new CurrentDoctorVisit();
                    visit.VistID = Convert.ToInt32(dt.Rows[i]["VistID"].ToString());
                    visit.VisitDate = Convert.ToString(dt.Rows[i]["Date"].ToString());
                    visit.ReasonOfVist = Convert.ToString(dt.Rows[i]["ReasonOfVist"].ToString());
                    //visit.Whathurts = Convert.ToString(dt.Rows[i]["WhatHurts"].ToString());
                    visit.Symptoms = Convert.ToString(dt.Rows[i]["Symptoms"].ToString());
                    visit.SymptomDurtion = Convert.ToString(dt.Rows[i]["SymptomDurtion"].ToString());
                    visit.DoctorID = Convert.ToInt32(dt.Rows[i]["DoctorID"].ToString());
                    visit.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                }
            }
            return visit;
        }
        public List<ChronicDisease> GetChronicDiseasesByPatientId(int id)
        {
            List<ChronicDisease> chronicDiseases = new List<ChronicDisease>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine) 
            {
                var p = GetChronicDiseaseByPatientId(id,i);
                if (p == null)
                {
                    endOfLine = true;
                }
                else
                {
                    chronicDiseases.Add(p);
                    i++;
                }
                
            }
            return chronicDiseases;
        }
        public List<CurrentDoctorVisit> GetCurrentDoctorVisitsByPatientId(int id)
        {
            List<CurrentDoctorVisit> visits = new List<CurrentDoctorVisit>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var a = GetCurrentDoctorVisitByPatientId(id, i);
                if (a == null)
                {
                    endOfLine = true;
                }
                else
                {
                    visits.Add(a);
                    i++;
                }

            }
            return visits;
        }
        public List<ChronicMedication> GetChronicMedicationsByPatientId(int id)
        {
            List<ChronicMedication> chronicMedications = new List<ChronicMedication>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var a = GetChronicMedicationByPatientId(id, i);
                if (a == null)
                {
                    endOfLine = true;
                }
                else
                {
                    chronicMedications.Add(a);
                    i++;
                }

            }
            return chronicMedications;
        }
        public List<AcuteMedication> GetAcuteMedicationsByPatientId(int id)
        {
            List<AcuteMedication> acuteMedications = new List<AcuteMedication>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var a = GetAcuteMedicationByPatientId(id, i);
                if (a == null)
                {
                    endOfLine = true;
                }
                else
                {
                    acuteMedications.Add(a);
                    i++;
                }

            }
            return acuteMedications;
        }
        public List<AcuteDisease> GetAcuteDiseasesByPatientId(int id)
        {
            List<AcuteDisease> acuteDiseases = new List<AcuteDisease>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var p = GetAcuteDiseaseByPatientId(id, i);
                if (p == null)
                {
                    endOfLine = true;
                }
                else
                {
                    acuteDiseases.Add(p);
                    i++;
                }

            }
            return acuteDiseases;
        }
        public List<Allergy> GetAllergiesByPatientId(int id)
        {
            List<Allergy> allergies = new List<Allergy>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var p = GetAllergyByPatientId(id, i);
                if (p == null)
                {
                    endOfLine = true;
                }
                else
                {
                    allergies.Add(p);
                    i++;
                }

            }
            return allergies;
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
        public Schedule GetScheduleById(int id)
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
                return new Schedule()
                {
                    ScheduleID = Convert.ToInt32(dt.Rows[0]["ScheduleID"].ToString()),
                    ScheduleNo = Convert.ToInt32(dt.Rows[0]["ScheduleNo"].ToString()),
                };
            }
            return new Schedule();
        }
        public Dosage GetDosageById(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetDosageById", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new Dosage()
                {
                    DosageID = Convert.ToInt32(dt.Rows[0]["DosageID"].ToString()),
                    FormName = Convert.ToString(dt.Rows[0]["FormName"].ToString()),
                };
            }
            return new Dosage();
        }
        public Medication_ActiveIngredient GetMedication_ActiveIngredientById(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetDosageById", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                return new Medication_ActiveIngredient()
                {
                    MedIngreID = Convert.ToInt32(dt.Rows[0]["MedIngreID"].ToString()),
                    ActiveStrength = Convert.ToString(dt.Rows[0]["ActiveStrength"].ToString()),
                    MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"].ToString()),
                    ActiveIngreID = Convert.ToInt32(dt.Rows[0]["ActiveIngreID"].ToString()),
                };
            }
            return new Medication_ActiveIngredient();
        }
        public List<Doctor> GetDoctors()
        {
            List<Doctor> visits = new List<Doctor>();
            connection();
            dbCmd = new SqlCommand("GetDoctors", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    visits.Add(
                        new Doctor()
                        {
                            DoctorId = Convert.ToInt32(current["DoctorId"].ToString()),
                            FirstName = Convert.ToString(current["FirstName"].ToString()),
                            LastName = Convert.ToString(current["LastName"].ToString()),
                            //Whathurts = Convert.ToString(current["Whathurts"].ToString()),
                            //SymptomDurtion = Convert.ToString(current["SymptomDurtion"].ToString()),
                            //Symptoms = Convert.ToString(current["Symptoms"].ToString()),
                            //DoctorID = Convert.ToInt32(current["DoctorID"].ToString()),
                            //PatientID = Convert.ToInt32(current["PatientID "].ToString()),
                        });
                }
            }
            return visits;
        }
    }
}
