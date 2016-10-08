using Coypu;

namespace PageObject.Elements
{
    public class Select : Element
    {
        public Select(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value => FindField().Value;

        public string Selected
        {
            get { return FindField().SelectedOption; }
            set { Browser.Select(value).From(Locator); }
        }
    }
}