using System;
using System.Collections.Generic;

namespace PageObject
{
    public class PageObjectAttribute : Attribute
    {
        public Type BasePage { get; }
        public string BaseUrl { get; }
        public string Path { get; }

        public PageObjectAttribute(Type basePage) : this(basePage, "")
        {
        }

       public PageObjectAttribute(Type basePage, string path) : this(path)
        {
            BasePage = basePage;
        }

        public PageObjectAttribute(string baseUrl, string path) : this(path)
        {
            BaseUrl = baseUrl;
        }

        public PageObjectAttribute(string path)
        {
            Path = path;
        }

        private static readonly IDictionary<Type, PageObjectAttribute> PageObjectAttributes = new Dictionary<Type, PageObjectAttribute>();

        internal static PageObjectAttribute For(Type pageClass)
        {
            if (!PageObjectAttributes.ContainsKey(pageClass))
            {
                foreach (var attribute in GetCustomAttributes(pageClass))
                {
                    if (attribute is PageObjectAttribute)
                        PageObjectAttributes[pageClass] = (PageObjectAttribute) attribute;
                }
            }

            return PageObjectAttributes[pageClass];
        }
    }
}
