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

        protected Page(PageSession session, Type basePage, string path = "")
        {
            BasePage = basePage;

            EnsureNoCircularReferencesInBasePages();

            Session = session;

            if (basePage == null)
            {
                Uri = UriBuilder.Build(path);
            }
            else
            {
                Uri = UriBuilder.Build(For(basePage).Uri, path);
            }

            Hosts = new List<string> { Uri.Host };
        }

        private void EnsureNoCircularReferencesInBasePages()
        {
            var basePage = BasePage;

            if (BasePage == GetType())
                throw new PageObjectException(string.Format("Page {0} cannot have itself as a base page", basePage));

            while (basePage != null)
            {
                var nextBasePage = For(basePage).BasePage;

                if (nextBasePage == GetType())
                    throw new PageObjectException(string.Format("Detected circular base page references with {0} and {1}", GetType(), basePage));

                basePage = nextBasePage;
            }
        }

        public Type BasePage { get; }

        private static readonly IDictionary<Type, Page> BasePages = new Dictionary<Type, Page>();

        private static Page For(Type pageClass)
        {
            if (!BasePages.ContainsKey(pageClass))
            {
                BasePages[pageClass] = null;
                BasePages[pageClass] = PageFactory.Instance.PageFor(pageClass);
            }
            else if (BasePages[pageClass] == null)
            {
                throw new PageObjectException(string.Format("Detected circular base page references with {0}", pageClass));
            }

            return BasePages[pageClass];
        }

        public void Visit()
        {
            Browser.Visit(Url);
            Session.Page = this;
        }
    }
}
