using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApplicationProject.Extensions
{
    public static class SessionExtensions
    {
        // Save an object to session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Retrieve an object from session
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
