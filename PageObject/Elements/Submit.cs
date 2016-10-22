using Coypu;

namespace PageObject.Elements
{
    public class Submit : HtmlElement
    {
        // TODO: Should restrict search to input submit buttons
        public Submit(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }
        public override ElementScope Element => SearchScope.FindButton(Locator);
    }
}
