using Coypu;

namespace PageObject.Elements
{
    public class RadioElement : InputElement
    {
        public RadioElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public void Choose()
        {
            Browser.Choose(Locator);
        }

        public bool Selected => FindField().Selected;
    }
}
