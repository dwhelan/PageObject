using Coypu;

namespace PageObject.Elements
{
    public class Select : InputElement
    {
        public Select(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return FindField().SelectedOption; }
            set { Browser.Select(value).From(Locator); }
        }
    }
}