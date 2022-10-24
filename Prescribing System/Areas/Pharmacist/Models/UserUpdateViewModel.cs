using Prescribing_System.Models;


namespace Prescribing_System.Areas.Pharmacist.Models
{
    public class UserUpdateViewModel
    {
        public PharmacistUser User { get; set; }
        public User UserDetails { get; set; }
        protected PharmacistDbcontext data = new PharmacistDbcontext();
        public UserUpdateViewModel()
        {
            User = data.GetPharmacistWithId(UserSingleton.GetLoggedUser().UserId);
        }
    }
}
