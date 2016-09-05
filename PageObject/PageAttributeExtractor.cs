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

        public string Path => PageAttribute.Path;

        private PageAttribute pageAttribute;
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

        private Type ParentPageClass => PageAttribute.Parent;

        private Uri ParentUri
        {
            get
            {
                if (parentUri != null)
                    return parentUri;

                if (ParentPageClass != null)
                    return parentUri = new PageAttributeExtractor(ParentPageClass).Uri;

                return null;
            }
        }

        internal PageAttribute PageAttribute
        {
            get
            {
                if (pageAttribute != null)
                    return pageAttribute;

                var attributes = Attribute.GetCustomAttributes(pageClass);
                foreach (var attribute in attributes)
                {
                    if (attribute is PageAttribute)
                    {
                        return pageAttribute = (PageAttribute) attribute;
                    }
                }
                throw new Exception(string.Format("Missing [Page] attribute for {0}", pageClass));
            }
        }
    }
}