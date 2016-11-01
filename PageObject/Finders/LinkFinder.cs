using Coypu;

namespace PageObject.Finders
{
    internal class LinkFinder : ElementFinder
    {
        internal LinkFinder(Driver driver, string locator, DriverScope scope, Options options) : base(driver, locator, scope, options)
        {
        }
    }
}
