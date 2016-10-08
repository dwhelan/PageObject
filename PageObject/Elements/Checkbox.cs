using Coypu;

namespace PageObject.Elements
{
    public class Checkbox : Element
    {
        public Checkbox(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Text => FindField().OuterScope.Text;

        public void Check()
        {
            Browser.Check(Locator);
        }

        public void Uncheck()
        {
            Browser.Uncheck(Locator);
        }

        public bool Checked()
        {
            return FindField().Selected;
        }
    }
}
