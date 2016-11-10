using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Coypu;
using System.Linq;

namespace PageObject
{
    public class ElementFinder
    {
        private readonly Coypu.Finders.ElementFinder finder;
        private readonly Type type;

        internal ElementFinder(string finderName, Driver driver, string locator, DriverScope scope, Options options)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var culture = CultureInfo.InvariantCulture;
            type = Type.GetType($"Coypu.Finders.{finderName}Finder, Coypu");
            var parameters = new object[] { driver, locator, scope, options };
            
            // ReSharper disable once AssignNullToNotNullAttribute
            finder = (Coypu.Finders.ElementFinder) Activator.CreateInstance(type, flags, null, parameters, culture);
        }

        internal Element Find(Options options)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var culture = CultureInfo.InvariantCulture;
            var parameters = new object[] { };
            var x = Type.GetType("Coypu.Finders.FinderOptionsDisambiguationStrategy, Coypu");

            // ReSharper disable once AssignNullToNotNullAttribute
            var foo = (DisambiguationStrategy) Activator.CreateInstance(x, flags, null, parameters, culture);
            return foo.ResolveQuery(finder);
        }

        internal IEnumerable<Element> FindAll(Options options)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var method = type.GetMethod("Find", flags);
            return ((IEnumerable<Element>)method.Invoke(finder, new object[] { options })).ToList();
        }
    }
}