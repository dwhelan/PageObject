using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace PageObject
{
    public class EnvironmentVariables
    {
        private static readonly Regex VariableRegex = new Regex(@"(?<=\${).*?(?=})");

        public static string Expand(string text)
        {
            if (text == null) return null;

            var result = text;

            foreach (Match match in VariableRegex.Matches(result))
            {
                string newValue;
                if (Lookup(match.Value, out newValue))
                    result = result.Replace("${" + match.Value + "}", newValue);
            }

            return result;
        }

        private static bool Lookup(string name, out string value)
        {
            value = name;

            if (LoadFromBuiltInVariables(name, ref value)) return true;
            if (LoadFromEnvironmentVariables(name, ref value)) return true;
            if (LoadFromAppConfig(name, ref value)) return true;

            return false;
        }

        private static bool LoadFromEnvironmentVariables(string name, ref string value)
        {
            foreach (var target in EnvironmentTargets())
            {
                if (Environment.GetEnvironmentVariable(name, target) != null)
                {
                    value = Environment.GetEnvironmentVariable(name, target);
                    return true;
                }
            }
            return false;
        }

        private static readonly IDictionary<string, string> BuiltInVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "cd",  Environment.CurrentDirectory }
        };

        private static bool LoadFromBuiltInVariables(string name, ref string value)
        {
            if (BuiltInVariables.ContainsKey(name))
            {
                value = BuiltInVariables[name];
                return true;
            }
            return false;
        }

        private static bool LoadFromAppConfig(string name, ref string value)
        {
            try
            {
                value = (string) new AppSettingsReader().GetValue(name, typeof(string));
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            
        }

        private static IEnumerable<EnvironmentVariableTarget> EnvironmentTargets()
        {
            return Enum.GetValues(typeof(EnvironmentVariableTarget)).Cast<EnvironmentVariableTarget>();
        }
    }
}
