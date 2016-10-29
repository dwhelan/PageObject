using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Coypu;

namespace PageObject.Finders
{
    internal class ElementFinder
    {
        protected readonly Coypu.Finders.ElementFinder finder;
        protected Type type;

        internal ElementFinder(Driver driver, string locator, DriverScope scope, Options options)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var culture = CultureInfo.InvariantCulture;
            type = Type.GetType($"Coypu.Finders.{GetType().Name}, Coypu");
            var parameters = new object[] { driver, locator, scope, options };
            // ReSharper disable once AssignNullToNotNullAttribute
            finder = (Coypu.Finders.ElementFinder) Activator.CreateInstance(type, flags, null, parameters, culture);
        }

        internal IEnumerable<Coypu.Element> Find(Options options)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var method = type.GetMethod("Find", flags);
            var elements = (IEnumerable<Coypu.Element>) method.Invoke(finder, new object[] { new Options() });
            return elements;
        }
    }
}