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

        protected Page(PageSession session, string url, string relativePath = "") : this(session, new Uri(url), relativePath)
        {
        }

        protected Page(PageSession session, Uri uri, string relativePath  = "")
        {
            Session = session;
            Uri = new Uri(uri, relativePath );
            Hosts = new List<string> { Uri.Host };
        }

        protected Page(PageSession session)
        {
            Session = session;
            Uri = new Uri(UrlAttributeValue);
            Hosts = new List<string> { Uri.Host };
        }

        private string UrlAttributeValue
        {
            get
            {
                var attributes = Attribute.GetCustomAttributes(GetType());
                foreach (var attribute in attributes)
                {
                    var pageAttribute = attribute as PageAttribute;
                    if (pageAttribute != null)
                    {
                        return pageAttribute.url;
                    }
                }

                throw new Exception("Missing attribute value!");
            }
        }

        public void Visit()
        {
            Browser.Visit(Url);
            Session.Page = this;
        }
    }
}