using System;
using System.Runtime.CompilerServices;
using Coypu;
using PageObject.Elements;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri => Attribute.Uri;
        public string Url => Uri.AbsoluteUri;
        public string Title => Browser.Title;

        public bool IsActive => new UriMatcher(this, Browser.Location).Matches;

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

        protected ElementScope FindField([CallerMemberName]string propertyName = "")
        {
            var attribute = PageAttribute(propertyName);
            return Browser.FindField(attribute.Locator);
        }

        protected FillInWith FillIn([CallerMemberName]string propertyName = "")
        {
            var attribute = PageAttribute(propertyName);
            return Browser.FillIn(attribute.Locator);
        }

        protected PageElementAttribute PageAttribute(string propertyName)
        {
            var property = GetType().GetProperty(propertyName);
            return (PageElementAttribute) property.GetCustomAttributes(typeof(PageElementAttribute), true)[0];
        }
    }
}
