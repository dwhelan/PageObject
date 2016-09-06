using System;

namespace PageObject
{
    internal class PageAttributeExtractor
    {
        public Uri Uri
        {
            get
            {
                if (ParentUri == null)
                    return new Uri(Path);

                return new Uri(ParentUri, Path);
            }
        }

        public string Path => PageObjectAttribute.Path;
        private string BaseUrl => PageObjectAttribute.BaseUrl;

        private PageObjectAttribute pageObjectAttribute;
        private readonly Type pageClass;
        private Uri parentUri;

        internal PageAttributeExtractor(Type pageClass)
        {
            this.pageClass = pageClass;

            if (!ValidParentPage())
                throw new ArgumentException(string.Format("The parent page for {0}, must be a subclass of {1}", pageClass, typeof(Page)));
        }

        private bool ValidParentPage()
        {
            return ParentPageClass == null || ParentPageClass.IsSubclassOf(typeof(Page));
        }

        private Type ParentPageClass => PageObjectAttribute.BaseParent;

        private Uri ParentUri
        {
            get
            {
                if (parentUri != null)
                    return parentUri;

                if (ParentPageClass != null)
                    return parentUri = new PageAttributeExtractor(ParentPageClass).Uri;

                if (BaseUrl != null)
                    return parentUri = new Uri(BaseUrl);

                return null;
            }
        }

        internal PageObjectAttribute PageObjectAttribute
        {
            get
            {
                if (pageObjectAttribute != null)
                    return pageObjectAttribute;

                var attributes = Attribute.GetCustomAttributes(pageClass);
                foreach (var attribute in attributes)
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