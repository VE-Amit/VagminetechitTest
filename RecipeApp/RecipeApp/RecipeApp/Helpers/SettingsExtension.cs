using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Xamarin.Essentials;

namespace RecipeApp.Helpers
{
    public class SettingsExtension
    {
        public static string GetStringForKey(string key)
        {
            var value = Preferences.Get(key, string.Empty);
            return value;
        }
        public static void AddOrUpdateStringForKey(string key, string value)
        {
            try
            {
                Preferences.Set(key, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving settings value: {ex}");
            }
        }
        public static T GetClassForKey<T>(string key, T @default) where T : class
        {
            string serialized = Preferences.Get(key, string.Empty);
            T result = @default;

            try
            {
                JsonSerializerSettings serializeSettings = GetSerializerSettings();
                result = JsonConvert.DeserializeObject<T>(serialized);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deserializing settings value: {ex}");
            }
            return result;
        }


        public static void AddClassForKey<T>(string key, T obj) where T : class
        {
            try
            {
                JsonSerializerSettings serializeSettings = GetSerializerSettings();
                string serialized = JsonConvert.SerializeObject(obj, serializeSettings);
                Preferences.Set(key, serialized);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error serializing settings value: {ex}");
            }
        }

        public static void DeleteClassForKey(string key)
        {
            try
            {
                Preferences.Remove(key);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error serializing settings value: {ex}");
            }
        }
        public static void DeletePreferenceKey(string key)
        {
            try
            {
                Preferences.Remove(key);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error serializing settings value: {ex}");
            }
        }

        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public const string AppDbSecret = "MRPDBSecret";
        public SettingsExtension()
        {
        }
    }
}
