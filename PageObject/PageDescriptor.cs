using System;
using System.Collections.Generic;

namespace PageObject
{
    internal class PageDescriptor
    {
        internal Uri Uri => BaseUri == null ? UriBuilder.Build(Path) : new Uri(BaseUri, Path);

        private PageObjectAttribute PageObjectAttribute => PageObjectAttribute.For(pageClass);
        private string Path => PageObjectAttribute.Path;
        private Type BasePage => PageObjectAttribute.BasePage;
        private string BaseUrl => PageObjectAttribute.BaseUrl;

        private readonly Type pageClass;
        private Uri baseUri;

        internal PageDescriptor(Type pageClass)
        {
            this.pageClass = pageClass;

            EnsureValidBasePage();
            EnsureNoCircularReferencesInBasePages();
        }

        private Uri BaseUri
        {
            get
            {
                if (baseUri == null)
                {
                    if (BasePage != null)
                        baseUri = For(BasePage).Uri;

                    if (BaseUrl != null)
                        baseUri = UriBuilder.Build(BaseUrl);
                }

                return baseUri;
            }
        }

        private void EnsureValidBasePage()
        {
            if (BasePage == null || BasePage.IsSubclassOf(typeof(Page)))
                return;

            throw new PageObjectException(string.Format("The base page for {0} must be a subclass of {1}", pageClass, typeof(Page)));
        }

        private void EnsureNoCircularReferencesInBasePages()
        {
            if (BasePage == pageClass)
                throw new PageObjectException(string.Format("Page {0} cannot have itself as a base page", pageClass));

            var basePage = BasePage;

            while (basePage != null)
            {
                var nextBasePage = PageObjectAttribute.For(basePage).BasePage;

                if (nextBasePage == pageClass)
                    throw new PageObjectException(string.Format("Detected circular base page references with {0} and {1}", pageClass, basePage));

                basePage = nextBasePage;
            }
        }

        private static readonly IDictionary<Type, PageDescriptor> PageDescriptors = new Dictionary<Type, PageDescriptor>();

        internal static PageDescriptor For(Type pageClass)
        {
            if (!PageDescriptors.ContainsKey(pageClass))
                PageDescriptors[pageClass] = new PageDescriptor(pageClass);

            return PageDescriptors[pageClass];
        }
    }
}
