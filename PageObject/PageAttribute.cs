using System;

namespace PageObject
{
    public class PageAttribute : Attribute
    {
        public readonly string url;

        public PageAttribute(string url)
        {
            this.url = url;
        }
    }
}