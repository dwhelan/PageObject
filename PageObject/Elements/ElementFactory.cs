using System;

namespace PageObject.Elements
{
    internal static class ElementFactory
    {
        public static T ElementFor<T>(Page page, string propertyName) where T : Element
        {
            var attribute = PropertyAttribute(page.GetType(), propertyName);
            return (T)Activator.CreateInstance(typeof(T), attribute, page.Browser);
        }

        private static ElementAttribute PropertyAttribute(Type type, string name)
        {
            var property = type.GetProperty(name);
            return (ElementAttribute) property.GetCustomAttributes(typeof(ElementAttribute), true)[0];
        }
    }
}
