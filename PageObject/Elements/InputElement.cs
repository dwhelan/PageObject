using Coypu;

namespace PageObject.Elements
{
    public abstract class InputElement : Element
    {
        protected InputElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public bool Disabled => FindField().Disabled;
        public bool Enabled => !Disabled;
        public string Text => FindField().OuterScope.Text;
    }
}
