using System;
using System.Text.RegularExpressions;

namespace PageObject
{
    internal class UriMatcher
    {
        private readonly Page page;
        private readonly Uri location;

        internal UriMatcher(Page page, Uri location)
        {
            this.page = page;
            this.location = location;
        }

        internal bool Matches()
        {
            return SchemeMatches() && HostMatches() && PathMatches();
        }

        private bool SchemeMatches()
        {
            return page.Uri.Scheme.Equals(location.Scheme);
        }

        private bool HostMatches()
        {
            if (page.Uri.Host.Equals(location.Host))
                return true;

            return Regex.IsMatch(location.Host, page.Attribute.HostMatch);
        }

        private bool PathMatches()
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