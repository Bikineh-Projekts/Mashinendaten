using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MaschinenDataein.Helper
{
    public static class SessionHelper
    {
        public static void SetObjectInSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetCustomObjectFromSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            // Falls kein Eintrag existiert oder String ist leer -> 'default(T)' = null bei Referenztypen
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            // JsonConvert.DeserializeObject kann ebenfalls null zurückgeben (z.B. ungültiges JSON)
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
