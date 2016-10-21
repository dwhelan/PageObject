using Coypu;

namespace PageObject.Elements
{
    public abstract class Input : HtmlElement
    {
        protected Input(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public bool Enabled => !Disabled;
        public bool Disabled => Element.Disabled;

        public override ElementScope Element => SearchScope.FindField(Locator);
    }
}
