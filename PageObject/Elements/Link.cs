using Coypu;
using PageObject.Finders;

namespace PageObject.Elements
{
    public class Link : Element
    {
        public Link(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        protected override ElementFinder Finder => new LinkFinder(Browser.Driver, Locator, SearchScope, new Options());
    }
}
