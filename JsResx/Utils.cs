using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsResx
{
    public static class Utils
    {
        /// <summary>
        /// Gets the constants of a type.
        /// </summary>
        /// <param name="type">The type.</param><returns></returns>
        public static List<FieldInfo> GetConstants(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var constants = fields.Where(x => x.IsLiteral && !x.IsInitOnly);
            return constants.ToList();
        }

        /// <summary>
        /// Creates a stringbuilder ready to work with the main variable name already set.
        /// </summary>
        /// <param name="mainVar">The main variable.</param>
        /// <returns></returns>
        public static StringBuilder MainVarBuilder(string mainVar)
        {
            return new StringBuilder($"var {mainVar}={{}};");
        }

        /// <summary>
        /// Filters the app settings.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> FilterAppSettings(string searchCriteria)
        {
            var entries = new List<KeyValuePair<string, string>>();
            if (ConfigurationManager.AppSettings.Count > 0)
            {
                var keys = ConfigurationManager.AppSettings.AllKeys;
                foreach (var key in keys)
                {
                    if (key.ToLower().IndexOf(searchCriteria.ToLower(), StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        entries.Add(new KeyValuePair<string, string>(key, GetString(key)));
                    }
                }
            }
            return entries;
        }

        /// <summary>
        /// Gets a string from web.config. 
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            return GetString(key, string.Empty);
        }

        /// <summary>
        /// Gets a string from web.config. 
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultvalue">Default value if key does not exist</param>
        /// <returns></returns>
        public static string GetString(string key, string defaultvalue)
        {
            var res = defaultvalue;
            if (ConfigurationManager.AppSettings[key] != null)
            {
                res = ConfigurationManager.AppSettings[key].ToString(CultureInfo.InvariantCulture);
            }
            return res;
        }
    }
}
