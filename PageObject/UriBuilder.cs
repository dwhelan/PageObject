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

        internal static Uri Build(Uri uri, string path)
        {
            try
            {
                return uri == null ? new Uri(path) : new Uri(uri, path);
            }
            catch (UriFormatException x)
            {
                throw new PageObjectException(String.Format(@"Invalid Uri ""{0}/{1}""", uri, path), x);
            }
        }

        internal static Uri Build(string url, string path)
        {
            try
            {
                return url == null ? new Uri(path) : new Uri(new Uri(url), path);
            }
            catch (UriFormatException x)
            {
                throw new PageObjectException(String.Format(@"Invalid Uri ""{0}/{1}""", url, path), x);
            }
        }
    }
}
