using System;

namespace PageObject
{
    public class PageAttribute : Attribute
    {
        public Type ParentPageClass { get; }
        public string Url { get; }

        public PageAttribute(string url)
        {
            Url = url;
        }

        public PageAttribute(Type parentPageClass, string url)
        {
            ParentPageClass = parentPageClass;
            Url = url;
        }

    }
}