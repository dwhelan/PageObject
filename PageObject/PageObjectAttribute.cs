using System;
using System.Collections.Generic;

namespace PageObject
{
    public class PageObjectAttribute : Attribute
    {
        public Type BasePage { get; }
        public string BaseUrl { get; }
        public string Path { get; }

        public PageObjectAttribute(string path, Type basePage) : this(path)
        {
            BasePage = basePage;
        }

        public PageObjectAttribute(string path, string baseUrl) : this(path)
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
                foreach (var attribute in Attribute.GetCustomAttributes(pageClass))
                {
                    if (attribute is PageObjectAttribute)
                        PageObjectAttributes[pageClass] = (PageObjectAttribute) attribute;
                }
            }

            return PageObjectAttributes[pageClass];
        }
    }
}
