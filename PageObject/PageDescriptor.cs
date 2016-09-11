using System;
using System.Collections.Generic;

namespace PageObject
{
    internal class PageDescriptor
    {
        internal bool HasBase => Attribute.BasePage != null || Attribute.BaseUrl != null;
        internal Uri Uri => UriBuilder.Build(BaseUri, Attribute.Path);

        internal PageAtAttribute Attribute => PageAtAttribute.For(pageClass);

        private readonly Type pageClass;
        private Uri baseUri;

        internal void EnsureNoBase()
        {
            if (Attribute.BasePage != null)
                throw new PageObjectException(
                    "Cannot specify a base Page, Uri or url in the constructor when you have included a base Page in the PageAt() attribute");

            if (Attribute.BaseUrl != null)
                throw new PageObjectException(
                    "Cannot specify a base Page, Uri or url in the constructor when you have included a base url in the PageAt() attribute");
        }
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
                    if (Attribute.BasePage != null)
                        baseUri = For(Attribute.BasePage).Uri;

                    if (Attribute.BaseUrl != null)
                        baseUri = UriBuilder.Build(Attribute.BaseUrl);
                }

                return baseUri;
            }
        }

        private void EnsureValidBasePage()
        {
            if (Attribute.BasePage == null || Attribute.BasePage.IsSubclassOf(typeof(Page)))
                return;

            throw new PageObjectException(string.Format("The base page for {0} must be a subclass of {1}", pageClass, typeof(Page)));
        }

        private void EnsureNoCircularReferencesInBasePages()
        {
            if (Attribute.BasePage == pageClass)
                throw new PageObjectException(string.Format("Page {0} cannot have itself as a base page", pageClass));

            var basePage = Attribute.BasePage;

            while (basePage != null)
            {
                var nextBasePage = PageAtAttribute.For(basePage).BasePage;

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
