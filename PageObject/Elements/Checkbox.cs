using Coypu;

namespace PageObject.Elements
{
    public class Checkbox : Input
    {
        public Checkbox(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public bool Value
        {
            get { return Base.Selected;}
            set { if (value) Select(); else Deselect(); }
        }

        public void Select()
        {
            Scope.Check(Locator);
        }

        public void Deselect()
        {
            Scope.Uncheck(Locator);
        }
    }
}
