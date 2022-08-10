using System.Text.Json;

namespace Skolplattformen.ElevApp.Data
{
    internal static class Storage
    {
        public static void Store(string key, object obj)
        {

            var value = JsonSerializer.Serialize(obj);


            Preferences.Default.Set(key, value);

        }

        public static T Get<T>(string key)
        {

            var value = Preferences.Get(key, (string)null);

            if (value == null) return default(T);

            var obj = JsonSerializer.Deserialize<T>(value);

            return obj;
        }

        public static void Delete(string key)
        {
            if (Preferences.ContainsKey(key))
            {
                Preferences.Remove(key);
            }
        }
    }
}
