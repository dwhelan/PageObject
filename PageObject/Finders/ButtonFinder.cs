using Coypu;

namespace PageObject.Finders
{
    internal class ButtonFinder : ElementFinder
    {
        internal ButtonFinder(Driver driver, string locator, DriverScope scope, Options options) : base(driver, locator, scope, options)
        {
        }
    }
}