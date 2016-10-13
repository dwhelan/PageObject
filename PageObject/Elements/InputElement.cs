using Coypu;

namespace PageObject.Elements
{
    public abstract class InputElement : Element
    {
        protected InputElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public bool Disabled => Element.Disabled;
        public bool Enabled => !Disabled;
        public string Text => Element.OuterScope.Text;

        protected ElementScope Element => Scope.FindField(Locator);
    }
}
