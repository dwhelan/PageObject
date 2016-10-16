using Coypu;

namespace PageObject.Elements
{
    public abstract class Input : HtmlElement
    {
        protected Input(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public bool Enabled => !Disabled;
        public bool Disabled => Scope.Disabled;

        protected string InputXPath(string type, string constraints = "")
        {
            return $".//input[@type='{type}' {constraints}]";
        }
    }
}
