using Coypu;

namespace PageObject.Elements
{
    public class SelectElement : InputElement
    {
        public SelectElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return FindField().SelectedOption; }
            set { Browser.Select(value).From(Locator); }
        }

        public void Select(string option)
        {
            Browser.Select(option).From(Locator);
        }
    }
}