using Coypu;

namespace PageObject.Elements
{
    public class Link : Element
    {
        public Link(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public override ElementScope ElementScope => SearchScope.FindLink(Locator);
    }
}
