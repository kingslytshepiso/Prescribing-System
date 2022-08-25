using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prescribing_System.Models
{
    public class RegisterViewModel
    {
        public Patient UserPatient { get; set; }
        public User UserDetails { get; set; }
        public GlobalDbContext data = new GlobalDbContext();
        public List<Suburb> Suburbs { get; set; }
        public List<City> Cities { get; set; }
        public List<Province> Provinces { get; set; }
        public RegisterViewModel()
        {
            Suburbs = data.GetAllSuburbs();
            Cities = data.GetAllCities();
            Provinces = data.GetAllProvinces();
        }
    }
}
