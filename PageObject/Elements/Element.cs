using Coypu;

namespace PageObject.Elements
{
    public abstract class Element
    {
        protected BrowserSession Browser { get; }
        protected ElementAttribute Attribute { get; }
        protected string Locator => Attribute.Locator;

        public Element(ElementAttribute attribute, BrowserSession browser)
        {
            Attribute = attribute;
            Browser = browser;
        }
    }
}