using Coypu;

namespace PageObject.Elements
{
    public class Text : Field
    {
        public Text(ElementAttribute attribute, BrowserSession browser, Options findOptions, Coypu.Element element = null) : base(attribute, browser, findOptions, element) { }

        public string Value
        {
            get { return CoypuElement.Value; }
            set { Driver.Set(CoypuElement, value); }
        }
    }
}
