using System.Collections.Generic;
using Coypu;

namespace PageObject.Elements
{
    public class Checkbox : Field
    {
        public Checkbox(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element)
        {
        }

        public bool Value
        {
            get { return CoypuElement.Selected;}
            set { if (value) Select(); else Deselect(); }
        }

        public void Select()
        {
            Driver.Check(CoypuElement);
        }

        public void Deselect()
        {
            Driver.Uncheck(CoypuElement);
        }

        public IList<bool> Options()
        {
            return new List<bool> {true, false};
        }
    }
}
