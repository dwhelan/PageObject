using Coypu;

namespace PageObject.Elements
{
    public abstract class Element
    {
        protected BrowserSession Browser { get; }
        protected string Locator => Attribute.Locator;

        private ElementAttribute Attribute { get; }
        public string Text => FindField().OuterScope.Text;

        protected Element(ElementAttribute attribute, BrowserSession browser)
        {
            Attribute = attribute;
            Browser = browser;
        }

        protected ElementScope FindField()
        {
            return Browser.FindField(Locator);
        }
    }
}