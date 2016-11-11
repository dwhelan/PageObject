using Coypu;

namespace PageObject.Elements
{
    public class Button : Element
    {
        public Button(ElementAttribute attribute, BrowserSession browser, Options findOptions, Coypu.Element element = null) : base(attribute, browser, findOptions, element) { }

        public override string FinderName => "ButtonFinder";
    }
}
