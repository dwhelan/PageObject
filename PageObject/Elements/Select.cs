using System.Collections.Generic;
using Coypu;

namespace PageObject.Elements
{
    public class Select : InputElement
    {
        public Select(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get { return Element.SelectedOption; }
            set { Scope.Select(value).From(Locator); }
        }

        public IList<string> Options => new List<string>();
    }
}