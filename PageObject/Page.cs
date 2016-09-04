using System;
using System.Collections.Generic;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri { get; }
        public List<string> Hosts { get; }
        protected readonly PageSession Session;

        protected Page(PageSession session, string url, string relativePath = "") : this(session, new Uri(url), relativePath)
        {
        }

        protected Page(PageSession session, Uri uri, string relativePath  = "")
        {
            Uri = new Uri(uri, relativePath );
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