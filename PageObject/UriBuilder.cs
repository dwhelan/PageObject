using System;

namespace PageObject
{
    internal class UriBuilder
    {
        internal static Uri Build(string url)
        {
            return Build((string) null, url);
        }

        internal static Uri Build(Uri uri, string path)
        {
            return Build(uri?.AbsoluteUri, path);
        }

        internal static Uri Build(string url, string path)
        {
            try
            {
                return url == null ? new Uri(path) : new Uri(new Uri(url), path);
            }
            catch (UriFormatException x)
            {
                if ((object) url == null)
                    throw new PageObjectException($@"Invalid url ""{path}""", x);
                throw new PageObjectException($@"Invalid url ""{url}/{path}""", x);
            }
        }

        public static bool AbsoluteUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
