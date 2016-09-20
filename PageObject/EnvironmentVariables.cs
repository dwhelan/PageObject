using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PageObject
{
    public class EnvironmentVariables
    {
        private static readonly Regex VariableRegex = new Regex(@"(?<={).*?(?=})");

        public static string Expand(string text)
        {
            if (text == null) return null;

            var result = text;

            foreach (Match match in VariableRegex.Matches(result))
                result = result.Replace("{" + match.Value + "}", Lookup(match.Value));

            return result;
        }

        private static string Lookup(string value)
        {
            if (value.Equals("cd", StringComparison.CurrentCultureIgnoreCase))
                return Environment.CurrentDirectory;

            foreach (var target in EnvironmentTargets())
            {
                if (Environment.GetEnvironmentVariable(value, target) != null)
                    return Environment.GetEnvironmentVariable(value, target);
            }

            return "foo";
        }

        private static IEnumerable<EnvironmentVariableTarget> EnvironmentTargets()
        {
            return Enum.GetValues(typeof(EnvironmentVariableTarget)).Cast<EnvironmentVariableTarget>();
        }
    }
}
