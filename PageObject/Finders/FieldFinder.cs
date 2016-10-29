using Coypu;

namespace PageObject.Finders
{
    internal class FieldFinder : ElementFinder
    {
        internal FieldFinder(Driver driver, string locator, DriverScope scope, Options options) : base(driver, locator, scope, options)
        {
        }
    }
}