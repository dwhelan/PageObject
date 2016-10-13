using Coypu;

namespace PageObject.Elements
{
    public abstract class Element
    {
        protected BrowserSession Browser { get; }
        protected Driver Driver => Browser.Driver;
        protected string Locator => Attribute.Locator;

        protected Element(ElementAttribute attribute, BrowserSession browser)
        {
            Attribute = attribute;
            Browser = browser;
        }

        protected ElementScope FindField()
        {
            return Scope.FindField(Locator);
        }

        private ElementAttribute Attribute { get; }
        protected Scope Scope => Browser;
    }
}
