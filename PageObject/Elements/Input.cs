using Coypu;

namespace PageObject.Elements
{
    public abstract class Input : Element
    {
        protected Input(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public bool Enabled => !Disabled;
        public bool Disabled => Base.Disabled;
        public string Text => Base.OuterScope.Text;

        protected string InputXPath(string type, string contraints = "")
        {
            return $".//input[@type='{type}' {contraints}]";
        }
    }
}
