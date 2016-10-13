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
            get { return SelectionValue; }
            set { Driver.Choose(RadioButtonFor(value)); }
        }

        private string SelectionValue
        {
            get
            {
                var selection = Selection;

                if (selection == null)
                    return "";

                var label = LabelTextFor(selection);

                return string.IsNullOrEmpty(label) ? selection.Value : label;
            }
        }

        private static string LabelTextFor(ElementScope selection)
        {
            var labels = selection.FindAllXPath($"//label[@for='{selection.Id}'] | .//parent::label");
            return string.Join(" ", labels.Select(label => label.Text));
        }

        public IList<string> Options => RadioButtons.Select(radioButton => radioButton.Value).ToList();

        private IEnumerable<ElementScope> RadioButtons => FindAllXPathOrThrow(RadioButtonsXpath(), "radio button");

        private ElementScope Selection => RadioButtons.FirstOrDefault(radioButton => radioButton.Selected);

        private ElementScope RadioButtonFor(string value)
        {
            return FindXPath(RadioButtonsXpath($"and ( {WithAncestorLabel(value)} or {WithLabelFor(value)} or {WithId(value)} or {WithValue(value)})"));
        }

        private string RadioButtonsXpath(string constraints = "")
        {
            return InputXPath("radio", $" and @name='{Locator}' {constraints}");
        }
    }
}
