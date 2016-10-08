using Coypu;

namespace PageObject.Elements
{
    public class Text : InputElement
    {
        public Text(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return FindField().Value; }
            set { Browser.FillIn(Locator).With(value);}
        }
    }
}
