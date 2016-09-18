using System;
using System.Collections.Generic;
using System.Linq;

namespace PageObject
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageAtAttribute : Attribute
    {
        public Type BasePage { get; }
        public string BaseUrl { get; }
        public string Path { get; }

        public PageAtAttribute(Type basePage) : this(basePage, "")
        {
        }

        public PageAtAttribute(Type basePage, string path) : this(path)
        {
            BasePage = basePage;
        }

        public PageAtAttribute(string baseUrl, string path) : this(path)
        {
            BaseUrl = baseUrl;
        }

        public PageAtAttribute(string path)
        {
            Path = path;
        }

        private static readonly IDictionary<Type, PageAtAttribute> PageObjectAttributes = new Dictionary<Type, PageAtAttribute>();

        internal static PageAtAttribute For(Type pageClass)
        {
            if (PageObjectAttributes.ContainsKey(pageClass))
                return PageObjectAttributes[pageClass];

            var attribute = GetCustomAttributes(pageClass).FirstOrDefault(a => a is PageAtAttribute);

            if (attribute == null)
                throw new PageObjectException(string.Format("Missing [PageAt(...))] attribute for {0}", pageClass));

            return PageObjectAttributes[pageClass] = (PageAtAttribute) attribute;
        }

        internal void EnsureValidBasePage(Type pageClass)
        {
            if (BasePage == null || BasePage.IsSubclassOf(typeof(Page)))
                return;

            throw new PageObjectException(string.Format("The base page for {0} must be a subclass of {1}", pageClass, typeof(Page)));
        }
    }
}
