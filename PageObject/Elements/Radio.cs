using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class Radio : InputElement
    {
        public Radio(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { var selection = Selection; return selection == null ? "" : selection.Value; }
            set { Driver.Choose(RadioButtonFor(value)); }
        }

        public IList<string> Options => RadioButtons.Select(radioButton => radioButton.Value).ToList();

        private IEnumerable<ElementScope> RadioButtons => FindAllXPathOrThrow(RadioButtonsXpath(), "radio button");

        private ElementScope Selection => RadioButtons.FirstOrDefault(radioButton => radioButton.Selected);

        private ElementScope RadioButtonFor(string value)
        {
            return FindXPath(RadioButtonsXpath($"and ({WithId(value)} or {WithValue(value)} or {WithAncestorLabel(value)} or {WithLabelFor(value)})"));
        }

        private string RadioButtonsXpath(string constraints = "")
        {
            return InputXPath("radio", $" and @name='{Locator}' {constraints}");
        }
    }
}
