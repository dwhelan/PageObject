using Coypu;

namespace PageObject.Elements
{
    public class Text : Field
    {
        public Text(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        public string Value
        {
            get { return CoypuElement.Value; }
            set { Driver.Set(CoypuElement, value); }
        }
    }
}
