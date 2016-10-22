using Coypu;

namespace PageObject.Elements
{
    public class Text : Input
    {
        public Text(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return Element.Value; }
            set { SearchScope.FillIn(Locator).With(value);}
        }
    }
}
