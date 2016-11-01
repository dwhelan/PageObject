using System.Linq;
using Coypu;
using PageObject.Finders;

namespace PageObject.Elements
{
    public abstract class Field : Element
    {
        protected Field(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        public bool Enabled => !Disabled;
        public bool Disabled => CoypuElement.Disabled;

        protected override ElementFinder Finder => new FieldFinder(Browser.Driver, Locator, SearchScope, new Options());
    }
}
