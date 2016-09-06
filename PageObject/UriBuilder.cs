using System;

namespace PageObject
{
    internal class UriBuilder
    {
        internal static Uri Build(string url)
        {
            try
            {
                return new Uri(url);
            }
            catch (UriFormatException x)
            {
                throw new PageObjectException(String.Format(@"Invalid url ""{0}""", url), x);
            }
        }
    }
}
