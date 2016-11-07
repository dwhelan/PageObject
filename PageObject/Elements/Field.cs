using Coypu;

namespace PageObject.Elements
{
    public abstract class Field : Element
    {
        protected Field(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        public bool Enabled  => !Disabled;
        public bool Disabled => CoypuElement.Disabled;

        public override string FinderName => "Field";
    }
}
