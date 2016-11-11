using Coypu;

namespace PageObject.Elements
{
    public abstract class Field : Element
    {
        protected Field(ElementAttribute attribute, BrowserSession browser, Options options, Coypu.Element element = null) 
            : base(attribute, browser, options, element) { }

        public bool Enabled  => !Disabled;
        public bool Disabled => CoypuElement.Disabled;

        public override string FinderName => "FieldFinder";
    }
}
