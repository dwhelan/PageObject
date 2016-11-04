using System;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class ElementList<T> : BaseElement where T : BaseElement
    {
        public ElementList(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public T this[int index]
        {
            get
            {
                var childElement = (T) Activator.CreateInstance(typeof(T), Attribute, Browser, null);
                var finder = new ElementFinder(childElement.FinderName, Browser.Driver, Locator, SearchScope, new Options());
                var list = finder.Find(new Options()).ToList();
                var e = (T) Activator.CreateInstance(typeof(T), Attribute, Browser, list[index]);
                return e;
            }
        }
    }
}