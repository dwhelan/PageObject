using System;
using System.Reflection;

namespace PageObject.Elements
{
    internal static class ElementFactory
    {
        public static T ElementFor<T>(Page page, string propertyName) where T : BaseElement
        {
            var attribute = ElementAttributeFor(page.GetType().GetProperty(propertyName));
            return (T) Activator.CreateInstance(typeof(T), attribute, page.Browser);
        }

        private static ElementAttribute ElementAttributeFor(PropertyInfo property)
        {
            return (ElementAttribute) property.GetCustomAttributes(typeof(ElementAttribute), true)[0];
        }
    }
}
