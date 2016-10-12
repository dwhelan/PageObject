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
            get
            {
                foreach (var radioButton in FindAllRadioButtons())
                {
                    if (radioButton.Selected)
                        return radioButton.Value;
                }
                return "";
            }
            set
            {
                Browser.Driver.Choose(FindRadioButton(value));
            }
        }

        private IEnumerable<SnapshotElementScope> FindAllRadioButtons()
        {
            var scopes = Browser.FindAllXPath($"//input[{RadioViaLocator(Locator)}]").ToList();
            if (scopes.Count == 0)
            {
                Browser.FindXPath($"//input[{RadioViaLocator(Locator)}]").HasValue("");
            }
            return scopes;
        }

        private string RadioViaLocator(string name) => $"@type='radio' and @name='{name}'";
        private string WithId(string id) { return $@"@id = '{id}'"; }
        private string WithValue(string value) { return $@"@value = '{value}'"; }
        private string WithAncestorLabel(string text) { return $@"ancestor::label[contains(normalize-space(text()),'{text}')]"; }
        private string WithLabelFor(string id) { return $@"@id = //label[contains(normalize-space(),'{id}')]/@for"; }

        private ElementScope FindRadioButton(string value)
        {
            var xpath = $@"//input
                [
                    {RadioViaLocator(Locator)} and 
                    ({WithId(value)} or {WithValue(value)} or {WithAncestorLabel(value)} or {WithLabelFor(value)})
                ]";
            return Browser.FindXPath(xpath);
        }
    }
}
