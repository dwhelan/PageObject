using Coypu;

namespace PageObject.Elements
{
    public class TextElement : Input
    {
        public TextElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return FindField().Value; }
            set { Browser.FillIn(Locator).With(value);}
        }
    }
}
