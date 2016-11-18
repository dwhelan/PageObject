using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Coypu;

namespace PageObject
{
    public class ElementFinder
    {
        private readonly Coypu.Finders.ElementFinder finder;
        private static readonly DisambiguationStrategy DisambiguationStrategy = (DisambiguationStrategy) CreateInstance("FinderOptionsDisambiguationStrategy");

        internal ElementFinder(string finderName, Driver driver, string locator, DriverScope scope, Options options)
        {
            finder = (Coypu.Finders.ElementFinder) CreateInstance(finderName, driver, locator, scope, options);
        }

        internal Element Find()
        {
            return DisambiguationStrategy.ResolveQuery(finder);
        }

        internal IEnumerable<Element> FindAll()
        {
            return (IEnumerable<Element>) Invoke(finder, "Find", finder.Options);
        }

        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        private static object CreateInstance(string typeName, params object[] parameters)
        {
            var type = Type.GetType($"Coypu.Finders.{typeName}, Coypu");

            // ReSharper disable once AssignNullToNotNullAttribute
            return Reflection.CreateInstance(type, parameters);
        }

        internal static object Invoke(object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, Flags);
            return method.Invoke(obj, parameters);
        }
    }

    internal static class Reflection
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        internal static object CreateInstance(Type type, params object[] parameters)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return Activator.CreateInstance(type, Flags, null, parameters, Culture);
        }
    }
}