using Coypu;

namespace PageObject.Elements
{
    public class Text : Input
    {
        public Text(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return Base.Value; }
            set { Scope.FillIn(Locator).With(value);}
        }
    }
}
