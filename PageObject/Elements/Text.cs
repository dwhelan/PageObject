using Coypu;
using System.Linq;
using PageObject.Finders;

namespace PageObject.Elements
{
    public class Text : Field
    {
        public Text(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return CoypuElement.Value; }
            set { Browser.Driver.Set(CoypuElement, value); }
        }

        protected Coypu.Element CoypuElement
        {
            get
            {
                var finder = new FieldFinder(Browser.Driver, Locator, SearchScope, new Options());
                return finder.Find(new Options()).ToList().First();
            }
        }
    }
}
