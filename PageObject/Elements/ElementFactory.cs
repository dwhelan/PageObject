using System;
using Coypu;

namespace PageObject.Elements
{
    internal static class ElementFactory
    {
        public static T ElementFor<T>(Page page, string propertyName, Options options = null) where T : BaseElement
        {
            var attribute = ElementAttributeFor(page, propertyName);
            return (T) Activator.CreateInstance(typeof(T), attribute, page.Browser, options, null);
        }

        public static ElementList<T> ElementListFor<T>(Page page, string propertyName) where T : BaseElement
        {
            var attribute = ElementAttributeFor(page, propertyName);
            return new ElementList<T>(attribute, page.Browser, new Options());
        }

        private static ElementAttribute ElementAttributeFor(Page page, string propertyName)
        {
            var property = page.GetType().GetProperty(propertyName);
            return (ElementAttribute) property.GetCustomAttributes(typeof(ElementAttribute), true)[0];
        }
    }
}
