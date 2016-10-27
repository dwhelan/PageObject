using Coypu;

namespace PageObject.Elements
{
    public class Text : Field
    {
        public Text(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return ElementScope.Value; }
            set { SearchScope.FillIn(Locator).With(value);}
        }
    }
}
