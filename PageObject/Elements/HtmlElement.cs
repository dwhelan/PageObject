using Coypu;

namespace PageObject.Elements
{
    public abstract class HtmlElement : Element
    {
        protected HtmlElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public abstract ElementScope Element { get; }

        public string Text => Element.Text;

        public void Click() { Element.Click(); }

        public void SendKeys(string keys) { Element.SendKeys(keys); }
    }
}
