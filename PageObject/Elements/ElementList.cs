using System;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class ElementList<T> : BaseElement where T : BaseElement
    {
        public ElementList(ElementAttribute attribute, BrowserSession browser, Options options) : base(attribute, browser, options) { }

        public T this[int index]
        {
            get
            {
                var childElement = (T) Activator.CreateInstance(typeof(T), Attribute, Browser, new Options(), null);
                var finder = new ElementFinder(childElement.FinderName, Browser.Driver, Locator, SearchScope, new Options());
                var list = finder.FindAll(new Options()).ToList();
                return (T) Activator.CreateInstance(typeof(T), Attribute, Browser, new Options(), list[index]);
            }
        }
    }
}