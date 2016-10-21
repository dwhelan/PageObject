using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class RadioButtons : Element
    {
        public RadioButtons(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get
            {
                var button = Buttons.FirstOrDefault(radioButton => radioButton.Selected);
                return button == null ? string.Empty : ValueOf(button);
            }
            set { Click(value); }
        }

        public IList<string> Options => Buttons.Select(ValueOf).ToList();

        public void Select(string value)
        {
            Click(value);
        }

        public void Click(string value)
        {
            try
            {
                Choose(ButtonWith(value));
            }
            catch (MissingHtmlException)
            {
                var radioButton = Buttons.First(button => StripWhitespace(LabelTextFor(button)).Equals(StripWhitespace(value)));
                Choose(radioButton);
            }
        }

        private string ValueOf(ElementScope radioButton)
        {
            var labelText = LabelTextFor(radioButton);
            return string.IsNullOrEmpty(labelText) ? radioButton.Value : labelText;
        }

        private IEnumerable<ElementScope> Buttons => FindAllXPathOrThrow(ButtonsXpath(), "radio button");

        private ElementScope ButtonWith(string value)
        {
            return FindXPath(ButtonsXpath($"and ({WithAncestorLabel(value)} or {WithLabelFor(value)} or {WithId(value)} or {WithValue(value)})"));
        }

        private string ButtonsXpath(string constraints = "")
        {
            return $".//input[@type='radio'and @name='{Locator}' {constraints}]";
        }

        private void Choose(Coypu.Element element)
        {
            Browser.Driver.Choose(element);
        }
    }
}
