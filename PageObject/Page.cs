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
            var extractor = new PageAttributeExtractor(this);
            Session = session;
            Uri = new Uri(extractor.Url);
            Hosts = new List<string> { Uri.Host };
        }

        public void Visit()
        {
            Browser.Visit(Url);
            Session.Page = this;
        }
    }

    internal class PageAttributeExtractor
    {
        private PageAttribute pageAttribute;

        internal PageAttributeExtractor(Page page)
        {
            Page = page;
        }

        public Page Page { get; set; }
        public string Url {  get { return PageAttribute.Url;  } }

        internal PageAttribute PageAttribute
        {
            get
            {
                if (pageAttribute != null)
                    return pageAttribute;

                var attributes = Attribute.GetCustomAttributes(Page.GetType());
                foreach (var attribute in attributes)
                {
                    if (attribute is PageAttribute)
                    {
                        return pageAttribute = (PageAttribute) attribute;
                    }
                }
                throw new Exception("Missing attribute value!");
            }
        }
    }
}