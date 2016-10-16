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
    }
}
