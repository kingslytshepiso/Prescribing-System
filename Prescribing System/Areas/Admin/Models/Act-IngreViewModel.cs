using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;
using Prescribing_System.Areas.Admin.Models.System_Objects;

namespace Prescribing_System.Areas.Admin.Models
{
    public class Act_IngreViewModel : ListViewModel
    {
        public List<ActiveIngredient> DataList;
    }
}
