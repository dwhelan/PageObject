using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Coypu;

namespace PageObject.Elements
{
    public abstract class Element : BaseElement
    {
        protected Element(ElementAttribute attribute, BrowserSession browser, Options findOptions, Coypu.Element element = null)
            : base(attribute, browser, findOptions)
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
                return Finder.Find(FindOptions);
            }
        }

        protected ElementScope FindXPath(string xPath, Options options = null)
        {
            return ElementScope.FindXPath(xPath, options);
        }

        private ElementFinder Finder => new ElementFinder(FinderName, Browser.Driver, Locator, SearchScope, new Options());

        protected SnapshotElementScope ElementScope
        {
            get
            {
                const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                var culture = CultureInfo.InvariantCulture;
                var parameters = new object[] { CoypuElement, SearchScope, new Options() };

                return (SnapshotElementScope) Activator.CreateInstance(typeof(SnapshotElementScope), flags, null, parameters, culture);
            }
        }

        public void Click() { Driver.Click(CoypuElement); }
        public void SendKeys(string keys) { Driver.SendKeys(CoypuElement, keys); }
    }
}
