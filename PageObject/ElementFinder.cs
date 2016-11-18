using System.Collections.Generic;
using Coypu;
using PageObject.Util;

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
            return (IEnumerable<Element>) Reflection.Invoke(finder, "Find", finder.Options);
        }

        private static object CreateInstance(string finderTypeName, params object[] parameters)
        {
            return Reflection.CreateInstance($"Coypu.Finders.{finderTypeName}, Coypu", parameters);
        }
    }
}