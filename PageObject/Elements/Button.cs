using Coypu;

namespace PageObject.Elements
{
    public class Button : Element
    {
        public Button(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        public override string FinderName => "Button";
    }
}
