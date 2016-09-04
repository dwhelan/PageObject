using System;
using System.Collections.Generic;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri { get; }
        public string Url => Uri.AbsolutePath;
        public List<string> Hosts { get; }
        public string Title => Session.Title;

        protected readonly PageSession Session;

        protected Page(PageSession session, string url, string relativePath = "") : this(session, new Uri(url), relativePath)
        {
        }

        protected Page(PageSession session, Uri uri, string relativePath  = "")
        {
            Session = session;
            Uri = new Uri(uri, relativePath );
            Hosts = new List<string> { Uri.Host };
        }

        public void Visit()
        {
            Session.Visit(Url);
        }

    }
}