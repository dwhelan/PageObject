using System;
using System.Text.RegularExpressions;

namespace PageObject
{
    internal static class UriMatcher
    {
        internal static bool UriMatches(Page page, Uri location)
        {
            return SchemeMatches(page, location) && HostMatches(page, location) && PathMatches(page, location);
            //var uri = page.Uri;
            //if (location == uri)
            //    return true;

            //var hostMatches = HostMatches(page, location);

            //if (SchemeMatches(page, location) && hostMatches)
            //{
            //    return PathMatches(page, location);
            //}

            //return false;
        }

        private static bool SchemeMatches(Page page, Uri location)
        {
            return page.Uri.Scheme.Equals(location.Scheme);
        }

        private static bool HostMatches(Page page, Uri location)
        {
            if (page.Uri.Host.Equals(location.Host))
                return true;

            return Regex.IsMatch(location.Host, page.Attribute.HostMatch);
        }

        private static bool PathMatches(Page page, Uri location)
        {
            return Path(page.Uri).Equals(Path(location));
        }

        private static string Path(Uri location)
        {
            var path = location.AbsolutePath;
            return path.StartsWith("/") ? path.Substring(1) : path;
        }
    }
}