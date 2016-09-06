using System;
using System.Collections.Generic;
using Coypu.Timing;

namespace PageObject
{
    internal class PageObjectAttributeExtractor
    {
        public Uri Uri => BaseUri == null ? BuildUri(Path) : new Uri(BaseUri, Path);

        private string Path => PageObjectAttribute.Path;
        private Type BasePage => PageObjectAttribute.BasePage;
        private string BaseUrl => PageObjectAttribute.BaseUrl;

        private PageObjectAttribute pageObjectAttribute;
        private readonly Type pageClass;
        private Uri baseUri;


        internal PageObjectAttributeExtractor(Type pageClass)
        {
            this.pageClass = pageClass;

            if (!ValidBasePage())
                throw new PageObjectException(string.Format("The base page for {0} must be a subclass of {1}", pageClass, typeof(Page)));

            EnsureNoCircularReferencesInBasePages();
        }

        private bool ValidBasePage()
        {
            return BasePage == null || BasePage.IsSubclassOf(typeof(Page));
        }

        private Uri BaseUri
        {
            get
            {
                if (baseUri != null)
                    return baseUri;

                if (BasePage != null)
                    return baseUri = new PageObjectAttributeExtractor(BasePage).Uri;

                if (BaseUrl != null)
                    return baseUri = BuildUri(BaseUrl);

                return null;
            }
        }

        private void EnsureNoCircularReferencesInBasePages()
        {
            if (BasePage == pageClass)
                throw new PageObjectException(string.Format("Page {0} cannot have itself as a base page", pageClass));

            var basePage = BasePage;

            while (basePage != null)
            {
                if (GetPageObjectAttribute(basePage).BasePage == pageClass)
                    throw new PageObjectException(string.Format("Detected circular base page references with {0} and {1}", pageClass, basePage));

                basePage = GetPageObjectAttribute(basePage).BasePage;
            }
        }

        private static Uri BuildUri(string url)
        {
            try
            {
                return new Uri(url);
            }
            catch (UriFormatException x)
            {
                throw new PageObjectException(string.Format(@"Invalid url ""{0}""", url), x);
            }
        }

        private PageObjectAttribute PageObjectAttribute
        {
            get
            {
                if (pageObjectAttribute == null)
                    pageObjectAttribute = GetPageObjectAttribute(pageClass);

                return pageObjectAttribute;
            }
        }

        private static PageObjectAttribute GetPageObjectAttribute(Type pageClass)
        {
            foreach (var attribute in Attribute.GetCustomAttributes(pageClass))
            {
                if (attribute is PageObjectAttribute)
                {
                    return (PageObjectAttribute)attribute;
                }
            }

            // Should never happen!
            throw new Exception(string.Format("Missing [Page] attribute for {0}", pageClass));
        }
    }
}