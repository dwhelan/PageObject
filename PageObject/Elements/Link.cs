using Coypu;

namespace PageObject.Elements
{
    public class Link : Element
    {
        public Link(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        public override string FinderName => "Link";
    }
}
