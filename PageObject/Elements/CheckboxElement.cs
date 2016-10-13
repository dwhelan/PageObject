using Coypu;

namespace PageObject.Elements
{
    public class CheckboxElement : InputElement
    {
        public CheckboxElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public bool Value
        {
            get { return Checked();}
            set { if (value) Check(); else Uncheck(); }
        }

        public void Check()
        {
            Scope.Check(Locator);
        }

        public void Uncheck()
        {
            Scope.Uncheck(Locator);
        }

        public bool Checked()
        {
            return Element.Selected;
        }
    }
}
