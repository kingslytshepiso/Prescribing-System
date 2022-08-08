using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models.System_Users;

namespace Prescribing_System.Areas.Admin.Models
{
    public class AddUserViewModel
    {
        public object SelectedUser { get; set; }
        public User UserDetails { get; set; }
        public List<object> Options = new List<object>();
        protected GlobalDbContext gData = new GlobalDbContext();
        public AddUserViewModel()
        {
            
        }
        public List<Suburb> GetSuburbs() => gData.GetAllSuburbs();
        public List<City> GetCities() => gData.GetAllCities();
        public List<Province> GetProvinces() => gData.GetAllProvinces();
    }
    public class AddPatientViewModel : AddUserViewModel
    {
        public PatientUser User { get; set; }
        public AddPatientViewModel()
        {
            User = (PatientUser)SelectedUser;
        }
        public PatientUser GetUser()
        {
            return (PatientUser)SelectedUser;
        }
    }
    public class AddPharmacistViewModel : AddUserViewModel
    {
        public PharmacistUser User { get; set; }
        public AddPharmacistViewModel()
        {
            User = (PharmacistUser)SelectedUser;
        }
        public PharmacistUser GetUser()
        {
            return (PharmacistUser)SelectedUser;
        }
    }
    public class AddDoctorViewModel : AddUserViewModel
    {
        public DoctorUser User { get; set; }
        public AddDoctorViewModel()
        {
            User = (DoctorUser)SelectedUser;
        }
        public DoctorUser GetUser()
        {
            return (DoctorUser)SelectedUser;
        }
    }
}
