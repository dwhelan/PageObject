using System.Collections.Generic;
using Coypu;

namespace PageObject.Elements
{
    public class SelectElement : Input
    {
        public SelectElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return Scope.SelectedOption; }
            set { Select(value); }
        }

        public void Select(string value)
        {
            SearchScope.Select(value).From(Locator);
        }

        public IList<string> Options => new List<string>();
    }
}