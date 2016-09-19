using System;
using System.Collections.Generic;
using System.Linq;

namespace PageObject
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageAtAttribute : Attribute
    {
        internal Uri Uri => UriBuilder.Build(BaseUri, Path);

        private Type BasePage { get; }
        private string BaseUrl { get; }
        private string Path { get; }
        public string HostMatch { get; set; }
        public string PathMatch { get; set; }

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

        internal Uri BaseUri
        {
            get
            {
                if (BasePage != null)
                    return For(BasePage).Uri;

                if (BaseUrl != null)
                    return UriBuilder.Build(BaseUrl);

                return null;
            }
        }

        internal void Validate(Type pageClass)
        {
            EnsureValidBasePage(pageClass);
            EnsureNoCircularReferencesInBasePages(pageClass);
            EnsureValidUri();
        }

        private void EnsureValidBasePage(Type pageClass)
        {
            if (BasePage == null || BasePage.IsSubclassOf(typeof(Page)))
                return;

            throw new PageObjectException(string.Format("The base page for {0} must be a subclass of {1}", pageClass, typeof(Page)));
        }

        private void EnsureNoCircularReferencesInBasePages(Type pageClass)
        {
            EnsureNoCircularReferencesInBasePages(pageClass, new List<Type>());
        }

        private void EnsureNoCircularReferencesInBasePages(Type pageClass, IList<Type> basePages)
        {
            if (basePages.Contains(pageClass))
            {
                basePages.Add(pageClass);
                throw new PageObjectException(string.Format("Detected circular base page references with {0}", string.Join(" => ", basePages.Select(p => p.FullName))));
            }

            var basePage = For(pageClass).BasePage;

            if (basePage != null)
            {
                basePages.Add(pageClass);
                EnsureNoCircularReferencesInBasePages(basePage, basePages);
            }
        }

        private void EnsureValidUri()
        {
            UriBuilder.Build(BaseUri, Path);
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
    }
}
