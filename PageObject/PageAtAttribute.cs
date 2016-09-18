using System;
using System.Collections.Generic;

namespace PageObject
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageAtAttribute : Attribute
    {
        public Type BasePage { get; }
        public string BaseUrl { get; }
        public string Path { get; }
        public bool HasABase => BasePage != null || BaseUrl != null;

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

            {
                foreach (var attribute in GetCustomAttributes(pageClass))
                {
                    if (attribute is PageAtAttribute)
                        return PageObjectAttributes[pageClass] = (PageAtAttribute)attribute;
                }
            }

            throw new PageObjectException(string.Format("Missing [PageAt(...))] attribute for {0}", pageClass));
        }
    }
}
