using System;
using System.Linq;
using Coypu;
using PageObject.Finders;

namespace PageObject.Elements
{
    public class ElementList<T> : BaseElement
    {
        public ElementList(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public T this[int index]
        {
            get
            {
                var finder = new FieldFinder(Browser.Driver, Locator, SearchScope, new Options());
                var list = finder.Find(new Options()).ToList();
                var e = (T) Activator.CreateInstance(typeof(T), Attribute, Browser, list[index]);
                return e;
            }
        }
    }
}