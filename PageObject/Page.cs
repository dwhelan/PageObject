using System;
using System.Runtime.CompilerServices;
using Coypu;
using PageObject.Elements;
using PageObject.Util;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri => Attribute.Uri;
        public string Url => Uri.AbsoluteUri;
        public string Title => Browser.Title;

        public bool IsActive => new UriMatcher(this, Browser.Location).Matches;

        protected internal BrowserSession Browser => Session.Browser;
        public PageSession Session { get; }

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

        public T Element<T>(Options options = null, [CallerMemberName] string propertyName = "") where T : BaseElement
        {
            return ElementFactory.ElementFor<T>(this, propertyName, options);
        }

        public ElementList<T> ElementList<T>([CallerMemberName]string propertyName = "") where T : BaseElement
        {
            return ElementFactory.ElementListFor<T>(this, propertyName);
        }
    }
}
