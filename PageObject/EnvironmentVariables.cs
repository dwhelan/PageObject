using System;
using System.Text.RegularExpressions;

namespace PageObject
{
    public class EnvironmentVariables
    {
        private static readonly Regex VariableRegex = new Regex(@"(?<={).*?(?=})");

        public static string Expand(string text)
        {
            var result = text;

            foreach (Match match in VariableRegex.Matches(result))
                result = result.Replace("{" + match.Value + "}", Lookup(match.Value));

            return result;
        }

        private static string Lookup(string value)
        {
            if (value.Equals("cd", StringComparison.CurrentCultureIgnoreCase))
                return Environment.CurrentDirectory;

            return Environment.GetEnvironmentVariable(value);
        }
    }
}