using Coypu;

namespace PageObject.Elements
{
    public abstract class HtmlElement : Element
    {
        protected HtmlElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public ElementScope Element => SearchScope.FindField(Locator);
        public string Text => Element.Text;

        public void Click()
        {
            Element.Click();
        }
    }
}
