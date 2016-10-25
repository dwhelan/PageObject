using Coypu;

namespace PageObject.Elements
{
    public abstract class HtmlElement : BaseElement
    {
        protected HtmlElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public abstract ElementScope Element { get; }

        public string Text  => Element.Text;
        public string Title => Element.Title;

        public void Click() { Element.Click(); }
        public void SendKeys(string keys) { Element.SendKeys(keys); }
    }
}
