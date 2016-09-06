using System;

namespace PageObject
{
    internal class PageObjectAttributeExtractor
    {
        public Uri Uri => BaseUri == null ? BuildUri(Path) : new Uri(BaseUri, Path);

        private string Path => PageObjectAttribute.Path;
        private Type BasePage => PageObjectAttribute.BasePage;
        private string BaseUrl => PageObjectAttribute.BaseUrl;
        private PageObjectAttribute PageObjectAttribute => PageObjectAttribute.For(pageClass);

        private readonly Type pageClass;
        private Uri baseUri;

        internal PageObjectAttributeExtractor(Type pageClass)
        {
            this.pageClass = pageClass;

            EnsureValidBasePage();
            EnsureNoCircularReferencesInBasePages();
        }

        private void EnsureValidBasePage()
        {
            if (BasePage == null || BasePage.IsSubclassOf(typeof(Page)))
                return;

            throw new PageObjectException(string.Format("The base page for {0} must be a subclass of {1}", pageClass, typeof(Page)));
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
                var nextBasePage = PageObjectAttribute.For(basePage).BasePage;

                if (nextBasePage == pageClass)
                    throw new PageObjectException(string.Format("Detected circular base page references with {0} and {1}", pageClass, basePage));

                basePage = nextBasePage;
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
    }
}
