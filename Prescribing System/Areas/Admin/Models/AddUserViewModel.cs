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

    public class AddPatientViewModel
    {
        public PatientUser User { get; set; }
        public User UserDetails { get; set; }
    }
    public class PharmacistDataModel
    {
        public PharmacistUser User { get; set; }
        public User UserDetails { get; set; }
        public PharmacistDataModel()
        {
            User = new PharmacistUser();
            UserDetails = new User();
        }
    }
    public class AddPharmacistViewModel : PharmacistDataModel
    {
        
    }
    public class AddDoctorViewModel
    {
        public DoctorUserGeneric User { get; set; }
        public User UserDetails { get; set; }
    }
}
