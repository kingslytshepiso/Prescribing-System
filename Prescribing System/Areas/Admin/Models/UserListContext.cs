using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prescribing_System.Models;

namespace Prescribing_System.Areas.Admin.Models
{
    public class UserListContext
    {
        public List<User> DataList;
        public int OverallCount = 0;
        public int CurrentPage = 1;
    }
}
