using System;
using System.Collections.Generic;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri { get; }
        public List<string> Hosts { get; }
        protected readonly PageSession Session;

        protected Page(PageSession session, string url) : this(session, new Uri(url))
        {
        }

        protected Page(PageSession session, Uri uri, string path) : this(session, new Uri(uri, path))
        {
        }

        protected Page(PageSession session, Uri uri)
        {
            Uri = uri;
            Session = session;
            Hosts = new List<string> { Uri.Host };
        }

        public void Visit()
        {
            Session.Visit(Uri.AbsoluteUri);
        }

        public string Title
        {
            get { return Session.Title; }
        }
    }
}