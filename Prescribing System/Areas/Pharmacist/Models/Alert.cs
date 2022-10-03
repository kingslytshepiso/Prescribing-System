using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Security.Permissions;

namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public string AlertType { get; set; }
        public int LineID { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string StatusReason { get; set; }
        public int UserID { get; set; }
        public string Extras { get; set; }
        public bool Ignored { get; set; }
        public string TempReason { get; set; }
    }
}
