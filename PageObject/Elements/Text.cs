using Coypu;

namespace PageObject.Elements
{
    public class Text : Element
    {
        public Text(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public bool Disabled => FindField().Disabled;
        public bool Enabled => !Disabled;

        public string Value
        {
            get { return FindField().Value; }
            set { FillIn(value); }
        }

        private void FillIn(string value)
        {
            Browser.FillIn(Locator).With(value);
        }
    }
}
