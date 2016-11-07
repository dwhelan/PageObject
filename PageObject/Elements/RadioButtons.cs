using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class RadioButtons : BaseElement
    {
        public RadioButtons(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser) { }

        public string Value
        {
            get { return Selection == null ? String.Empty : ValueOf(Selection); }
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

        public void SendKeys(string keys)
        {
            Selection?.SendKeys(keys);
        }

        private ElementScope Selection => Buttons.FirstOrDefault(radioButton => radioButton.Selected);
    
        private string ValueOf(ElementScope radioButton)
        {
            var labelText = LabelTextFor(radioButton);
            return String.IsNullOrEmpty(labelText) ? radioButton.Value : labelText;
        }

        private IEnumerable<ElementScope> Buttons => FindAllXPathOrThrow(ButtonsXpath(), "radio button");

        private ElementScope ButtonWith(string value)
        {
            return FindXPath(ButtonsXpath($"and ({WithAncestorLabel(value)} or {WithLabelFor(value)} or {WithId(value)} or {WithValue(value)})"), SearchScope);
        }

        private string ButtonsXpath(string constraints = "")
        {
            return $".//input[@type='radio'and @name='{Locator}' {constraints}]";
        }

        private void Choose(Coypu.Element element)
        {
            Browser.Driver.Choose(element);
        }

        protected static string LabelTextFor(ElementScope element)
        {
            var allLabels = element.FindAllXPath($"//label[@for='{element.Id}'] | .//parent::label");
            return string.Join(" ", allLabels.Select(label => label.Text));
        }

        protected static string WithId(string id)              { return $"@id = '{id}'"; }
        protected static string WithValue(string value)        { return $"@value = '{value}'"; }
        protected static string WithAncestorLabel(string text) { return $"ancestor::label[{WithText(text)}]"; }
        protected static string WithLabelFor(string id)        { return $"@id = //label[contains(normalize-space(),'{id}')]/@for"; }
    }
}
