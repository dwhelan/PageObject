using System;
using System.Linq;
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

        internal bool Matches() => SchemeMatches && HostMatches && PathMatches;

        private bool SchemeMatches => page.Uri.Scheme.Equals(location.Scheme) || page.Attribute.SchemeMatch.Contains(location.Scheme);
        private bool HostMatches => page.Uri.Host.Equals(location.Host)       || Regex.IsMatch(location.Host, page.Attribute.HostMatch);
        private bool PathMatches => Path(page.Uri).Equals(Path(location))     || Regex.IsMatch(Path(location), page.Attribute.PathMatch);

        private static string Path(Uri location)
        {
            var path = location.AbsolutePath;
            return path.StartsWith("/") ? path.Substring(1) : path;
        }
    }
}
