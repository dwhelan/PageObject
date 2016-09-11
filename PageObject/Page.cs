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

        protected Page(PageSession session, string urlOrPath)
        {
            Session = session;

            if (UriBuilder.AbsoluteUrl(urlOrPath))
            {
                Descriptor.EnsureNoBase();
                Uri = UriBuilder.Build(urlOrPath);
            }
            else if (Descriptor.HasBase)
            {
                Uri = UriBuilder.Build(Descriptor.Uri, urlOrPath);
            }
            else
            {
                Uri = UriBuilder.Build(urlOrPath);
            }

            Hosts = new List<string> { Uri.Host };
        }

        protected Page(PageSession session, string url, string path)
        {
            Session = session;

            if (!string.IsNullOrEmpty(url))
                Descriptor.EnsureNoBase();

            Uri = UriBuilder.Build(url, path);
            Hosts = new List<string> { Uri.Host };
        }

        protected Page(PageSession session)
        {
            Session = session;
            Uri = Descriptor.Uri;
            Hosts = new List<string> { Uri.Host };
        }

        protected Page(PageSession session, Type basePage)
        {
            Session = session;
            Uri = PageDescriptor.For(basePage).Uri;
            Hosts = new List<string> { Uri.Host };
        }

        public void Visit()
        {
            Browser.Visit(Url);
            Session.Page = this;
        }
    }
}
