using System.Linq;
using Coypu;
using PageObject.Finders;

namespace PageObject.Elements
{
    public abstract class Element : BaseElement
    {
        protected Element(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null)
            : base(attribute, browser)
        {
            coypuElement = element;
        }

        private readonly Coypu.Element coypuElement;

        public string Text  => CoypuElement.Text;
        public string Title => CoypuElement.Title;
        protected Driver Driver => Browser.Driver;

        public Coypu.Element CoypuElement
        {
            get
            {
                if (coypuElement != null) return coypuElement;
                return Finder.Find(new Options()).ToList().First();
            }
        }

        protected abstract ElementFinder Finder { get; }

        public void Click() { Driver.Click(CoypuElement); }
        public void SendKeys(string keys) { Driver.SendKeys(CoypuElement, keys); }
    }
}
