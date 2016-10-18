using System.Collections.Generic;
using Coypu;

namespace PageObject.Elements
{
    public class Checkbox : Input
    {
        public Checkbox(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public bool Value
        {
            get { return Scope.Selected;}
            set { if (value) Select(); else Deselect(); }
        }

        public void Select()
        {
            SearchScope.Check(Locator);
        }

        public void Deselect()
        {
            SearchScope.Uncheck(Locator);
        }

        public void Click()
        {
           Scope.Click();
        }

        public IList<bool> Options()
        {
            return new List<bool> {true, false};
        }
    }
}
