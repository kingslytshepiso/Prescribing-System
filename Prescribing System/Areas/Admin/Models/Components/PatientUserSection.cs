using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prescribing_System.Areas.Admin.Models.System_Users;
using Newtonsoft.Json;

namespace Prescribing_System.Areas.Admin.Models.Components
{
    public class PatientUserSection : ViewComponent
    {
        private PatientDataModel model { get; set; }
        public IViewComponentResult Invoke(AddUserViewModel userModel)
        {
            var modelSerialized = JsonConvert.SerializeObject(userModel);
            model = JsonConvert.DeserializeObject<PatientDataModel>(modelSerialized);
            var userSerialized = JsonConvert.SerializeObject(userModel.SelectedUser);
            return View(model);
        }
    }
}
