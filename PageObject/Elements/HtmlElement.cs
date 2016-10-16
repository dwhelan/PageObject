using Coypu;

namespace PageObject.Elements
{
    public abstract class HtmlElement : Element
    {
        protected HtmlElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public virtual ElementScope Base => Scope.FindField(Locator);
        public string Text => Base.Text;
    }
}
