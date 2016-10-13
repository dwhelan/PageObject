using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class Radio : Input
    {
        public Radio(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return ValueOf(Selection); }
            set { Driver.Choose(RadioButtonWith(value)); }
        }

        private string ValueOf(ElementScope radioButton)
        {
            if (radioButton == null)
                return "";

            var labelText = LabelTextFor(radioButton);

            return string.IsNullOrEmpty(labelText) ? radioButton.Value : labelText;
        }

        private static string LabelTextFor(ElementScope element)
        {
            var labels = element.FindAllXPath($"//label[@for='{element.Id}'] | .//parent::label");
            return string.Join(" ", labels.Select(label => label.Text));
        }

        public IList<string> Options => RadioButtons.Select(radioButton => radioButton.Value).ToList();

        private IEnumerable<ElementScope> RadioButtons => FindAllXPathOrThrow(RadioButtonsXpath(), "radio button");

        private ElementScope Selection => RadioButtons.FirstOrDefault(radioButton => radioButton.Selected);

        private ElementScope RadioButtonWith(string value)
        {
            return FindXPath(RadioButtonsXpath($"and ( {WithAncestorLabel(value)} or {WithLabelFor(value)} or {WithId(value)} or {WithValue(value)})"));
        }

        private string RadioButtonsXpath(string constraints = "")
        {
            return InputXPath("radio", $" and @name='{Locator}' {constraints}");
        }
    }
}
