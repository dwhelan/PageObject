using System;
using Coypu;

namespace PageObject.Elements
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementAttribute : Attribute
    {
        public string Locator { get; }

        public ElementAttribute(string locator)
        {
            Locator = locator;
        }
    }
}