using Coypu;

namespace PageObject.Elements
{
    public class Link : Element
    {
        public Link(ElementAttribute attribute, BrowserSession browser, Options findOptions, Coypu.Element element = null) : base(attribute, browser, findOptions, element) { }

        public override string FinderName => "Link";
    }
}
