using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PageObject
{
    internal class UriMatcher
    {
        private readonly Page page;
        private readonly Uri location;
        private PageAtAttribute Attribute => page.Attribute;
        private Uri Uri => page.Uri;

        internal UriMatcher(Page page, Uri location)
        {
            this.page = page;
            this.location = location;
        }

        internal bool Matches() => SchemeMatches && PortMatches && HostMatches && PathMatches;

        private bool SchemeMatches => Uri.Scheme.Equals(location.Scheme)    || Attribute.SchemeMatch.Contains(location.Scheme);
        private bool PortMatches   => Uri.Port.Equals(location.Port)        || Attribute.PortMatch.Contains(location.Port);
        private bool HostMatches   => Uri.Host.Equals(location.Host)        || Regex.IsMatch(location.Host, Attribute.HostMatch);
        private bool PathMatches   => Path(page.Uri).Equals(Path(location)) || Regex.IsMatch(Path(location), Attribute.PathMatch);

        private static string Path(Uri uri)
        {
            var path = uri.AbsolutePath;
            return path.StartsWith("/") ? path.Substring(1) : path;
        }
    }
}
