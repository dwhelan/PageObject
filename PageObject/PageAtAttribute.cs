using System;
using System.Collections.Generic;

namespace PageObject
{
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
            if (!PageObjectAttributes.ContainsKey(pageClass))
            {
                foreach (var attribute in GetCustomAttributes(pageClass))
                {
                    if (attribute is PageAtAttribute)
                        PageObjectAttributes[pageClass] = (PageAtAttribute) attribute;
                }
            }

            return PageObjectAttributes[pageClass];
        }
    }
}
