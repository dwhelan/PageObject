using System;

namespace PageObject.Elements
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PageElementAttribute : Attribute
    {
        public string Locator { get; }

        public PageElementAttribute(string locator)
        {
            Locator = locator;
        }
    }
}