using System;
using System.Collections.Generic;

namespace PageObject
{
    internal class PageDescriptor
    {
        internal Uri Uri => UriBuilder.Build(BaseUri, Attribute.Path);

        private PageAtAttribute Attribute { get; }

        internal PageDescriptor(Type pageClass)
        {
            Attribute = PageAtAttribute.For(pageClass);
            Attribute.EnsureValidBasePage(pageClass);
            EnsureNoCircularReferencesInBasePages(pageClass);
        }

        private Uri BaseUri
        {
            get
            {
                if (Attribute.BasePage != null)
                    return For(Attribute.BasePage).Uri;

                if (Attribute.BaseUrl != null)
                    return UriBuilder.Build(Attribute.BaseUrl);

                return null;
            }
        }

        private void EnsureNoCircularReferencesInBasePages(Type pageClass)
        {
            if (Attribute.BasePage == pageClass)
                throw new PageObjectException(String.Format("Page {0} cannot have itself as a base page", pageClass));

            var basePage = Attribute.BasePage;

            while (basePage != null)
            {
                var nextBasePage = PageAtAttribute.For(basePage).BasePage;

                if (nextBasePage == pageClass)
                    throw new PageObjectException(String.Format("Detected circular base page references with {0} and {1}", pageClass, basePage));

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
