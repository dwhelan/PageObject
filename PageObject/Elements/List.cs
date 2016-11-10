using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class List : Field
    {
        public List(ElementAttribute attribute, BrowserSession browser, Options findOptions, Coypu.Element element = null) : base(attribute, browser, findOptions, element) { }

        public string Value
        {
            get { return CoypuElement.SelectedOption; }
            set { Click(value); }
        }

        public void Select(string value)
        {
            Click(value);
        }

        public IEnumerable<string> Options => ElementScope.FindAllXPath(".//option").Select(option => option.Text);

        public void Click(string value)
        {
            SearchScope.Select(value).From(Locator);
        }
    }
}
