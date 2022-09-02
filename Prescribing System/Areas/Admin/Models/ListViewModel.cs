using System.Collections.Generic;

namespace Prescribing_System.Areas.Admin.Models
{
    public abstract class ListViewModel
    {
        public int OverallCount = 0;
        public int CurrentPage = 1;
        public string Sort = "none";
    }
}
