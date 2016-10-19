using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class SelectElement : Input
    {
        public SelectElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return Element.SelectedOption; }
            set { Click(value); }
        }

        public void Select(string value)
        {
            Click(value);
        }

        public IList<string> Options => FindAllXPathOrThrow(".//option", "option").Select(option => option.Text).ToList();

        public void Click(string value)
        {
            SearchScope.Select(value).From(Locator);
        }
    }
}
