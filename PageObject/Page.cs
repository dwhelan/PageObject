using System;
using System.Text.RegularExpressions;
using Coypu;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri => Attribute.Uri;
        public string Url => Uri.AbsoluteUri;
        public string Title => Browser.Title;

        protected BrowserSession Browser => Session.Browser;
        protected PageSession Session { get; }

        internal PageAtAttribute Attribute => PageAtAttribute.For(GetType());

        protected Page(PageSession session)
        {
            Session = session;
            Attribute.Validate(GetType());
        }

        public void Visit()
        {
            Browser.Visit(Url);
            Session.Page = this;
        }

        public bool IsActive
        {
            get {
                return UriMatcher.UriMatches(this, Browser.Location);
            }
        }
    }
}
