using Coypu;

namespace PageObject.Elements
{
    public abstract class Element : BaseElement
    {
        protected Element(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public abstract ElementScope ElementScope { get; }

        public string Text  => ElementScope.Text;
        public string Title => ElementScope.Title;
        protected Driver Driver => Browser.Driver;

        public void Click() { ElementScope.Click(); }
        public void SendKeys(string keys) { ElementScope.SendKeys(keys); }
    }
}
