using System;
using System.Collections.Generic;
using Coypu;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri { get; }
        public string Url => Uri.AbsoluteUri;
        public List<string> Hosts { get; }
        public string Title => Browser.Title;

        protected BrowserSession Browser => Session.Browser;
        protected PageSession Session { get; }

        private PageDescriptor Descriptor => PageDescriptor.For(GetType());

        protected Page(PageSession session, Page basePage, string path = "") : this(session, basePage?.Url, path)
        {
        }

        protected Page(PageSession session, Uri uri, string path = "") : this(session, uri?.AbsoluteUri, path)
        {
        }

        protected Page(PageSession session, string url, string path = "")
        {
            Session = session;
            Uri = UriBuilder.Build(url, path);
            Hosts = new List<string> { Uri.Host };
        }

        protected Page(PageSession session)
        {
            Session = session;
            Uri = Descriptor.Uri;
            Hosts = new List<string> { Uri.Host };
        }

        public void Visit()
        {
            Browser.Visit(Url);
            Session.Page = this;
        }
    }
}
