using Coypu;

namespace PageObject.Elements
{
    public abstract class Element
    {
        public BrowserSession Browser { get; }
        protected string Locator => Attribute.Locator;

        private ElementAttribute Attribute { get; }
 
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