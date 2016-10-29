using System.Collections.Generic;
using Coypu;

namespace PageObject.Elements
{
    public class Checkbox : Field
    {
        public Checkbox(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public bool Value
        {
            get { return ElementScope.Selected;}
            set { if (value) Select(); else Deselect(); }
        }

        public void Select()
        {
            Driver.Check(ElementScope);
        }

        public void Deselect()
        {
            Driver.Uncheck(ElementScope);
        }

        public IList<bool> Options()
        {
            return new List<bool> {true, false};
        }
    }
}
