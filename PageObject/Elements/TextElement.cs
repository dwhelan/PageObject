using Coypu;

namespace PageObject.Elements
{
    public class TextElement : InputElement
    {
        public TextElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return FindField().Value; }
            set { Scope.FillIn(Locator).With(value);}
        }
    }
}
