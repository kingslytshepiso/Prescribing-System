using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Prescribing_System.Models
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session,
            string key, string value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static string GetObject(this ISession session,
            string key)
        {
            var valueJson = session.GetString(key);
            if (string.IsNullOrEmpty(valueJson))
            {
                return new string("none");
            }
            else
                return JsonConvert.DeserializeObject(valueJson).ToString();
        }
    }
}
