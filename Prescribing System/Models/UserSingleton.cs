using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prescribing_System.Models
{
    public class UserSingleton
    {
        private static UserSingleton uniqueInstance;
        private static User CurrentUser;
        private UserSingleton() { }
        public static UserSingleton getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new UserSingleton();
            return uniqueInstance;
        }
        public static void LogUser(User user)
        {
            CurrentUser = user;
        }
        public static User GetLoggedUser()
        {
            if (CurrentUser == null)
                return new User();
            else
                return CurrentUser;
        }
        public static void Reset()
        {
            uniqueInstance = null;
            CurrentUser = null;
        }
    }
}
