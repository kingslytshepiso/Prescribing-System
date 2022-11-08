using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;
using Prescribing_System.Areas.Patient.Models;
//using Prescribing_System.Areas.Pharmacist.Models;
//using Prescribing_System.Areas.Doctor.Models;



namespace Prescribing_System.Areas.Patient.Models
{
    public class PatientDbcontext
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
        public List<PatientDisease> GetPatientDiseasesByPatientId(int id)
        {
            List<PatientDisease> chronicDiseases = new List<PatientDisease>();
            bool endOfLine = false;
            int i = 0;
            while (!endOfLine)
            {
                var p = GetPatientDiseaseByPatientId(id, i);
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
        public PatientDisease GetPatientDiseaseByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetChronicDiseaseByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PatientDisease Disease = null;
            if (dt.Rows.Count > 0)
            {
                if (i < dt.Rows.Count)
                {
                    Disease = new PatientDisease();
                    Disease.PatientDiseaseID = Convert.ToInt32(dt.Rows[i]["PatientDiseaseID"].ToString());
                    Disease.Date = Convert.ToDateTime(dt.Rows[i]["DiagnosisDate"].ToString());
                    Disease.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    Disease.DiseaseID = Convert.ToInt32(dt.Rows[i]["DiseaseID"].ToString());
                }
            }
            return Disease;
        }
        public List<PatientMedication> GetChronicMedicationsByPatientId(int id)
        {
            List<PatientMedication> chronicMedications = new List<PatientMedication>();
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
        public PatientMedication GetChronicMedicationByPatientId(int id, int i = 0)
        {
            connection();
            dbCmd = new SqlCommand("GetChronicMedicationByPatientId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PatientMedication chronic = null;
            if (dt.Rows.Count > 0)
            {
                if (i < dt.Rows.Count)
                {
                    chronic = new PatientMedication();
                    chronic.ChronicMedID = Convert.ToInt32(dt.Rows[i]["PatientMedID"].ToString());
                    chronic.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    chronic.MedicationID = Convert.ToInt32(dt.Rows[i]["MedicationID"].ToString());
                    chronic.MedIngreID = Convert.ToInt32(dt.Rows[i]["MedIngreID"].ToString());
                    chronic.ActiveIngredientID = Convert.ToInt32(dt.Rows[i]["ActiveIngredientID"].ToString());
                    chronic.DosageID = Convert.ToInt32(dt.Rows[i]["DosageID"].ToString());
                    chronic.Date = Convert.ToString(dt.Rows[i]["Date"].ToString());
                }
            }
            return chronic;
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

        internal bool AddPrescriptionLine(PatientPrescriptionViewModel model)
        {
            throw new NotImplementedException();
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
        public PatientUser GetPatientWithIdNo(int number)
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
                if (i < dt.Rows.Count)
                {
                    prescription = new Prescription();
                    prescription.PrescriptionID = Convert.ToInt32(dt.Rows[i]["PrescriptionID"].ToString());
                    prescription.Date = Convert.ToDateTime(dt.Rows[i]["Date"].ToString());
                    prescription.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    prescription.DoctorID = Convert.ToInt32(dt.Rows[i]["DoctorID"].ToString());
                    prescription.DoctorName = GetDoctorWithId(prescription.DoctorID).getName();
                    prescription.Status = Convert.ToString(dt.Rows[i]["Status"].ToString());
                }
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
                    line.LineID = Convert.ToInt32(dt.Rows[i]["LineID"].ToString());
                    line.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                    line.Instruction = Convert.ToString(dt.Rows[i]["Instruction"].ToString());
                    line.RepeatNo = Convert.ToInt32(dt.Rows[i]["RepeatNo"].ToString());
                    line.RepeatLeftNo = Convert.ToInt32(dt.Rows[i]["RepeatLeftNo"].ToString());
                    line.Status = Convert.ToString(dt.Rows[i]["Status"].ToString());
                    line.PrescriptionID = Convert.ToInt32(dt.Rows[i]["PrescriptionID"].ToString());
                    line.MedicationID = Convert.ToInt32(dt.Rows[i]["MedicationID"].ToString());
                    line.PharmacyID = Convert.ToInt32(dt.Rows[i]["PharmacyID"].ToString());
                }
            }
            return line;
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
                            scheduleNo = Convert.ToInt32(current["ScheduleNo"].ToString()),
                        });
                }
            }
            return schedules;
        }
        public ListMedViewModel SearchMedWithPaging(string keyword, int pageNumber,
            int pageSize)
        {
            ListMedViewModel medication = new ListMedViewModel();
            connection();
            if (keyword == "all")
            {
                dbCmd = new SqlCommand("GetAllMedWithPaging", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                dbCmd = new SqlCommand("SearchMedWithPaging", conn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.Parameters.AddWithValue("@Keyword", keyword);
            }
            dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
            DataSet ds = new DataSet();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(ds);
            conn.Close();
            if (ds.Tables.Count > 0)
            {
                medication.DataList = new List<Med_Ingred>();
                medication.Meds = GetAllMeds();
                medication.Ingredients = GetAllActiveIngredients();
                medication.Dosages = GetAllDosages();
                medication.Schedules = GetAllSchedules();
                medication.OverallCount = ds.Tables.Count;
                medication.CurrentPage = pageNumber;
                foreach (DataRow current in ds.Tables[pageNumber - 1].Rows)
                {
                    medication.DataList.Add(
                        new Med_Ingred()
                        {
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngredientID"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                            ActiveStrength = Convert.ToInt32(current["ActiveStrength"].ToString())
                        });
                }
            }
            return medication;
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
                    prescription.YearStr = prescription.Date.ToString("yyyy");
                    prescription.MonthStr = prescription.Date.ToString("MM");
                    prescription.dayStr = prescription.Date.ToString("dd");
                    prescription.PatientID = Convert.ToInt32(dt.Rows[i]["PatientID"].ToString());
                    prescription.DoctorID = Convert.ToInt32(dt.Rows[i]["DoctorID"].ToString());
                    prescription.Status = Convert.ToString(dt.Rows[i]["Status"].ToString());
                    prescription.DoctorName = GetDoctorWithId(prescription.DoctorID).getName();
                }
            }
            return prescription;
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
        public PatientPrescriptionViewModel GetAllPresciptionLines(int patientId)
        {
            PatientPrescriptionViewModel Lines = new PatientPrescriptionViewModel();
            connection();
            dbCmd = new SqlCommand("GetAllPresciptionLinesbyID", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@PatientID", patientId);
            //dbCmd.Parameters.AddWithValue("@DoctorID", doctorId);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Lines.DataList.Add(
                        new GenericPrescriptionLine()
                        {
                            Line = new PrescriptionLine()
                            {
                                LineID = Convert.ToInt32(row["LineID"].ToString()),
                                DosageID = Convert.ToInt32(row["DosageID"].ToString()),
                                MedicationID = Convert.ToInt32(row["MedicationID"].ToString()),
                                Quantity = Convert.ToInt32(row["Quantity"].ToString()),
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
                            Patient = new PatientUser()
                            {
                                PatientId = Convert.ToInt32(row["PatientID"].ToString()),
                                FirstName = Convert.ToString(row["FirstName"].ToString()),
                                LastName = Convert.ToString(row["LastName"].ToString()),
                            },
                            //Doctor = new DoctorUser()
                            //{
                            //    DoctorId = Convert.ToInt32(row["DoctorID"].ToString()),
                            //    FirstName = Convert.ToString(row["FirstName"].ToString()),
                            //    LastName = Convert.ToString(row["LastName"].ToString()),
                            //    MedPracId = Convert.ToInt32(row["MedPracID"].ToString()),
                            //    HighestQual = Convert.ToString(row["HighestQualification"].ToString()),
                            //},
                            Dosage = new Dosage()
                            {
                                DosageID = Convert.ToInt32(row["DosageID"].ToString()),
                                FormName = Convert.ToString(row["FormName"].ToString()),
                            },
                            Medication = new Medication()
                            {
                                MedicationID = Convert.ToInt32(row["MedicationID"].ToString()),
                                Name = Convert.ToString(row["Name"].ToString()),
                                ScheduleID = Convert.ToInt32(row["ScheduleID"].ToString()),
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
                            }
                        });

                }

            }
            return Lines;
        }
        public List<Med_Ingred> GetAllMedicationIngredient()
        {
            List<Med_Ingred> meds = new List<Med_Ingred>();
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
                        new Med_Ingred()
                        {
                            MedIngreID = Convert.ToInt32(current["MedIngreID"].ToString()),
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngredientID"].ToString()),
                            ActiveStrength = Convert.ToDouble(current["ActiveStrength"].ToString()),
                        });
                }
            }
            return meds;
        }
        public bool AddPrescription(PatientPrescriptionViewModel viewModel, int doctorID, int patientID, string nowDate)
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
        //public PatientChronicDiseaseModel GetAllPatientChronicDisease(int patientID, int pageNumber, int pageSize)
        //{
        //    PatientChronicDiseaseModel chronics = new PatientChronicDiseaseModel();
        //    connection();
        //    dbCmd = new SqlCommand("GetAllPatientChronicDisease", conn);
        //    dbCmd.CommandType = CommandType.StoredProcedure;
        //    dbCmd.Parameters.AddWithValue("@PatientID", patientID);
        //    dbCmd.Parameters.AddWithValue("@PageSize", pageSize);
        //    // dbCmd.Parameters.AddWithValue("@DoctorID", doctorID);
        //    ds = new DataSet();
        //    dbAdapter = new SqlDataAdapter(dbCmd);
        //    dbAdapter.Fill(ds);
        //    conn.Close();
        //    if (ds.Tables.Count > 0)
        //    {
        //        chronics.chronicDiseaseModels = new List<ListPatientChronicDisease>();
        //        chronics.OverallCount = ds.Tables.Count;
        //        chronics.CurrentPage = pageNumber;
        //        foreach (DataRow row in ds.Tables[pageNumber - 1].Rows)
        //        {
        //            chronics.chronicDiseaseModels.Add(
        //                new ListPatientChronicDisease()
        //                {
        //                    Patient = new Prescribing_System.Models.Patient()
        //                    {
        //                        PatientID = Convert.ToInt32(row["PatientID"].ToString()),
        //                        FirstName = Convert.ToString(row["FirstName"].ToString()),
        //                        LastName = Convert.ToString(row["LastName"].ToString()),
        //                    },
        //                    ChronicDisease = new ChronicDisease()
        //                    {
        //                        ChronicDiseaseID = Convert.ToInt32(row["ChronicMedHistoryID"].ToString()),
        //                        Date = Convert.ToString(row["Date"].ToString()),

        //                    },
        //                    Disease = new Disease()
        //                    {
        //                        DiseaseID = Convert.ToInt32(row["DiseaseID"].ToString()),
        //                        DiseaseName = Convert.ToString(row["Name"].ToString()),
        //                    },
        //                    //Doctor = new Doctor()
        //                    //{
        //                    //    DoctorId = Convert.ToInt32(row["DoctorID"].ToString()),
        //                    //    FirstName = Convert.ToString(row["FirstName"].ToString()),
        //                    //    LastName = Convert.ToString(row["LastName"].ToString()),
        //                    //    MedPracId = Convert.ToInt32(row["MedPracID"].ToString()),
        //                    //    HighestQual = Convert.ToString(row["HighestQualification"].ToString()),
        //                    //}

        //                });
        //        }
        //        return chronics;
        //    }
        //    return chronics;
        //}
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
                            Name = Convert.ToString(row["Name"].ToString()),
                        });
                }
            }
            return diseases;

        }
    }
}
