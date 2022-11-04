using Prescribing_System.Models;

namespace Prescribing_System.Areas.Doctor.Models
{
    public class UserUpdateViewModel
    {
        public Doctor User { get; set; }
        public User UserDetails { get; set; }
        protected DoctorDbContext data = new DoctorDbContext();
        public UserUpdateViewModel()
        {
            User = data.GetDoctorWithId(UserSingleton.GetLoggedUser().UserId);
        }
    }
}
