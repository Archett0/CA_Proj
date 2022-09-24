using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CA_Proj.Controllers
{
        public static class SessionExtends
        {
            public static void SetObject<T>(this ISession session, string key, T obj)
            {
            System.Console.WriteLine("inserting session key:{0} ,value:{1}", key, obj.ToString());
                if (obj.GetType().IsGenericType && obj.GetType().GetGenericTypeDefinition() == typeof(List<>))
                {
                    session.SetString(key, JsonConvert.SerializeObject(obj));
                }
                session.SetString(key,obj.ToString());
            }
            public static T GetObject<T>(this ISession session, string key)
            {
                T result = default(T);
                var value = session.GetString(key);
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<T>(value);
                    }
                    catch
                    {
                        return result;   
                    }
                }
                return result;
            }

        }

}
