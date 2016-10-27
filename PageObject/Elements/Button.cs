using Coypu;

namespace PageObject.Elements
{
    public class Button : Element
    {
        // TODO: Should restrict search to input submit buttons
        public Button(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }
        public override ElementScope ElementScope => SearchScope.FindButton(Locator);
    }
}
