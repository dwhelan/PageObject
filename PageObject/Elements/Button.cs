using Coypu;
using PageObject.Finders;

namespace PageObject.Elements
{
    public class Button : Element
    {
        public Button(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        protected override ElementFinder Finder => new ButtonFinder(Browser.Driver, Locator, SearchScope, new Options());
    }
}
