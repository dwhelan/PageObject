using Coypu;

namespace PageObject.Elements
{
    public abstract class Field : Element
    {
        protected Field(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public bool Enabled => !Disabled;
        public bool Disabled => ElementScope.Disabled;

        public override ElementScope ElementScope => SearchScope.FindField(Locator);
    }
}
