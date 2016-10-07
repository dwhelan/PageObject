using Coypu;

namespace PageObject.Elements
{
    public class Select : Element
    {
        public Select(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return FindField().Value; }
            set { Browser.Select("Second option"); }
        }

        private void FillIn(string value)
        {
            Browser.FillIn(Locator).With(value);
        }

        private ElementScope FindField()
        {
           return Browser.FindField(Locator);
        }

        public string Selected
        {
            get { return FindField().SelectedOption; }
            set { Browser.Select(value).From(Locator); }
        }
    }
}