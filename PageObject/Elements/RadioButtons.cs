using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class RadioButtons : Input
    {
        public RadioButtons(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return ValueOf(SelectedButton); }
            set { Choose(value); }
        }

        private void Choose(string value)
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

        protected void Choose(Coypu.Element radioButton)
        {
            Driver.Choose(radioButton);
        }

        public string StripWhitespace(string text)
        {
            return text.Replace(" ", "");
        }

        public IList<string> Options => Buttons.Select(ValueOf).ToList();

        private string ValueOf(ElementScope radioButton)
        {
            if (radioButton == null)
                return "";

            var labelText = LabelTextFor(radioButton);

            return string.IsNullOrEmpty(labelText) ?  radioButton.Value : labelText;
        }

        private IEnumerable<ElementScope> Buttons => FindAllXPathOrThrow(ButtonsXpath(), "radio button");

        private ElementScope SelectedButton => Buttons.FirstOrDefault(radioButton => radioButton.Selected);

        private ElementScope ButtonWith(string value)
        {
            return FindXPath(ButtonsXpath($"and ( {WithAncestorLabel(value)} or {WithLabelFor(value)} or {WithId(value)} or {WithValue(value)})"));
        }

        private string ButtonsXpath(string constraints = "")
        {
            return InputXPath("radio", $" and @name='{Locator}' {constraints}");
        }
    }
}
