using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Prescribing_System.Models
{
    public class MySession
    {
        /*The session key value that is used to get/retreive
         *the ID of the user thats logged in*/
        private const string UserKey = "user";
        private const string RoleKey = "role";
        private ISession session { get; set; }
        public MySession(ISession sess)
        {
            session = sess;
        }
        /*The method used to get/retreive the value of the assigned
         * user's ID or role*/
        public string GetUser() =>
            session.GetObject(UserKey) ?? new string("none");
        public string GetRole() =>
            session.GetObject(RoleKey) ?? new string("none");
        /*The method used to set/log the user and assign their ID
         *to the session value*/
        public void SetUser(string user, string role)
        {
            session.SetObject(UserKey, user);
            session.SetObject(RoleKey, role);
        }
        /*Method used to check if there is a value in the session state
         *by using the GetUser method.*/
        public bool UserLogged()
        {
            bool logged = false;
            string user = GetUser();
            if (user == null || user == "")
                logged = false;
            else
                logged = true;
            return logged;
        }
        /*Method used to remove the session value, thus logging the user off*/
        public void KillSession()
        {
            session.Remove(UserKey);
            session.Remove(RoleKey);
        }
    }
}
