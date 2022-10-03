using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Prescribing_System.Models;
using Prescribing_System.Areas.Pharmacist.Models;
//using Prescribing_System.Areas.Admin.Models.System_Objects;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class PharmacistDbcontext
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
        public bool DispenseMedication(PrescriptionLine line)
        {
            connection();

            dbCmd = new SqlCommand("DispenseMedication", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@LineID", line.LineID);
            dbCmd.Parameters.AddWithValue("@PharmacyID", line.PharmacyID);
            dbCmd.Parameters.AddWithValue("@Date", DateTime.Today);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool AddAlerts(List<Alert> alerts)
        {
            return true;
        }
        public bool RejectPrescriptionLine(PrescriptionLine line, string message)
        {
            connection();

            dbCmd = new SqlCommand("RejectLine", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@LineID", line.LineID);
            dbCmd.Parameters.AddWithValue("@PharmacyID", line.PharmacyID);
            dbCmd.Parameters.AddWithValue("@Date", DateTime.Today);
            dbCmd.Parameters.AddWithValue("@StatusMessage", message);
            conn.Open();
            int i = dbCmd.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public PharmacistUser GetPharmacistWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPharmacistWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PharmacistUser user = new PharmacistUser();
            if (dt.Rows.Count > 0)
            {
                user.PharmacistId = Convert.ToInt32(dt.Rows[0]["PharmacistID"].ToString());
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
                }
            }
            return prescription;
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
                    line.RepeatLeft = Convert.ToInt32(dt.Rows[i]["RepeatLeftNo"].ToString());
                    line.Status = Convert.ToString(dt.Rows[i]["Status"].ToString());
                    line.PrescriptionID = Convert.ToInt32(dt.Rows[i]["PrescriptionID"].ToString());
                    line.MedicationID = Convert.ToInt32(dt.Rows[i]["MedicationID"].ToString());
                    line.StatusMessage = Convert.ToString(dt.Rows[i]["StatusMessage"].ToString());
                    line.PharmacyID = Convert.ToInt32(dt.Rows[i]["PharmacyID"].ToString());
                    line.LastDispensed = Convert.ToDateTime(dt.Rows[i]["LastDispensed"].ToString());
                }
            }
            return line;
        }
        public PrescriptionLine GetPrescLineWithId(int id)
        {
            connection();
            dbCmd = new SqlCommand("GetPrescLineWithId", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            PrescriptionLine line = null;
            if (dt.Rows.Count > 0)
            {
                line = new PrescriptionLine();
                line.LineID = Convert.ToInt32(dt.Rows[0]["LineID"].ToString());
                line.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"].ToString());
                line.Instruction = Convert.ToString(dt.Rows[0]["Instruction"].ToString());
                line.RepeatNo = Convert.ToInt32(dt.Rows[0]["RepeatNo"].ToString());
                line.RepeatLeft = Convert.ToInt32(dt.Rows[0]["RepeatLeftNo"].ToString());
                line.Status = Convert.ToString(dt.Rows[0]["Status"].ToString());
                line.PrescriptionID = Convert.ToInt32(dt.Rows[0]["PrescriptionID"].ToString());
                line.MedicationID = Convert.ToInt32(dt.Rows[0]["MedicationID"].ToString());
                line.StatusMessage = Convert.ToString(dt.Rows[0]["StatusMessage"].ToString());
                line.PharmacyID = Convert.ToInt32(dt.Rows[0]["PharmacyID"].ToString());
                line.LastDispensed = Convert.ToDateTime(dt.Rows[0]["LastDispensed"].ToString());
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
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngreID"].ToString()),
                            //DosageID = Convert.ToInt32(current["DosageID"].ToString()),
                            ActiveStrength = Convert.ToInt32(current["ActiveStrength"].ToString()),
                            ScheduleID = Convert.ToInt32(current["ScheduleID"].ToString()),
                        });
                }
            }
            return meds;
        }
        public List<MedicationInteraction> GetAllInteraction()
        {
            List<MedicationInteraction> model = new List<MedicationInteraction>();
            connection();
            dbCmd = new SqlCommand("GetAllInteractions", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    model.Add(
                        new MedicationInteraction()
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
        public List<PatientDisease> GetAllPatientDiseases()
        {
            List<PatientDisease> diseases = new List<PatientDisease>();
            connection();
            dbCmd = new SqlCommand("GetAllPatientDiseases", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    diseases.Add(
                        new PatientDisease()
                        {
                            PatientDiseaseID = Convert.ToInt32(current["PatientDiseaseID"].ToString()),
                            DateDiagnosed = Convert.ToDateTime(current["DiagnosisDate"].ToString()),
                            PatientID = Convert.ToInt32(current["PatientID"].ToString()),
                            DiseaseID = Convert.ToInt32(current["DiseaseID"].ToString()),
                        });
                }
            }
            return diseases;
        }
        public List<PatientMedication> GetAllPatientMedications()
        {
            List<PatientMedication> medications = new List<PatientMedication>();
            connection();
            dbCmd = new SqlCommand("GetAllPatientMedications", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    medications.Add(
                        new PatientMedication()
                        {
                            PatientMedID = Convert.ToInt32(current["PatientMedID"].ToString()),
                            Date = Convert.ToDateTime(current["Date"].ToString()),
                            PatientID = Convert.ToInt32(current["PatientID"].ToString()),
                            MedicationID = Convert.ToInt32(current["MedicationID"].ToString()),
                        });
                }
            }
            return medications;
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
        public List<ContraIndication> GetAllContraIndications()
        {
            List<ContraIndication> contras = new List<ContraIndication>();
            connection();
            dbCmd = new SqlCommand("GetAllContra", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    contras.Add(
                        new ContraIndication()
                        {
                            ContraIndicaID = Convert.ToInt32(current["ContraIndicaID"].ToString()),
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngredientID"].ToString()),
                            DiseaseID = Convert.ToInt32(current["DiseaseID"].ToString()),
                            Description = Convert.ToString(current["Description"].ToString()),
                        });
                }
            }
            return contras;
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
        public List<Allergy> GetAllergies()
        {
            List<Allergy> allergies = new List<Allergy>();
            connection();
            dbCmd = new SqlCommand("GetAllAllergies", conn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dt = new DataTable();
            dbAdapter = new SqlDataAdapter(dbCmd);
            dbAdapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow current in dt.Rows)
                {
                    allergies.Add(
                        new Allergy()
                        {
                            AllergyID = Convert.ToInt32(current["AllergyID"].ToString()),
                            ActiveIngredientID = Convert.ToInt32(current["ActiveIngredientID"].ToString()),
                            PatientID = Convert.ToInt32(current["PatientID"].ToString())
                        }); ;
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
    }
}
