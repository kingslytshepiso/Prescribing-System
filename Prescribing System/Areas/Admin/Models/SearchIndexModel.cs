using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Areas.Admin.Models
{
    public class SearchIndexModel
    {
        public List<SearchObject> Objects = new List<SearchObject>();
        public string Type { get; set; }
        public string Keyword { get; set; }
    }
    public class SearchObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
