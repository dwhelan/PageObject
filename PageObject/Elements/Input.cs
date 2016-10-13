using Coypu;

namespace PageObject.Elements
{
    public abstract class Input : Element
    {
        protected Input(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public bool Enabled => !Disabled;
        public bool Disabled => Element.Disabled;
        public string Text => Element.OuterScope.Text;

        protected ElementScope Element => Scope.FindField(Locator);

        protected string InputXPath(string type, string contraints = "")
        {
            return $".//input[@type='{type}' {contraints}]";
        }
    }
}
