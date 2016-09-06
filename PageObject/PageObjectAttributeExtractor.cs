using System;

namespace PageObject
{
    internal class PageObjectAttributeExtractor
    {
        public Uri Uri => BaseUri == null ? new Uri(Path) : new Uri(BaseUri, Path);

        private string Path     => PageObjectAttribute.Path;
        private Type   BasePage => PageObjectAttribute.BasePage;
        private string BaseUrl  => PageObjectAttribute.BaseUrl;

        private PageObjectAttribute pageObjectAttribute;
        private readonly Type pageClass;
        private Uri baseUri;

        internal PageObjectAttributeExtractor(Type pageClass)
        {
            this.pageClass = pageClass;

            if (!ValidBasePage())
                throw new ArgumentException(string.Format("The base page for {0}, must be a subclass of {1}", pageClass, typeof(Page)));
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
                    return baseUri = new Uri(BaseUrl);

                return null;
            }
        }

        private PageObjectAttribute PageObjectAttribute
        {
            get
            {
                if (pageObjectAttribute != null)
                    return pageObjectAttribute;

                foreach (var attribute in Attribute.GetCustomAttributes(pageClass))
                {
                    if (attribute is PageObjectAttribute)
                    {
                        return pageObjectAttribute = (PageObjectAttribute) attribute;
                    }
                }
                throw new Exception(string.Format("Missing [Page] attribute for {0}", pageClass));
            }
        }
    }
}