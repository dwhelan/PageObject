using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using Coypu.Drivers;

namespace PageObject.Elements
{
    public class RadioSelectElement : InputElement
    {
        public RadioSelectElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

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
            var scopes = Browser.FindAllXPath($"//input[{RadioViaLocatorXPath(Locator)}]");
            if (scopes.ToList().Count == 0)
            {
                Browser.FindXPath($"//input[{RadioViaLocatorXPath(Locator)}]").HasValue("");
            }
            return scopes;
        }

        private string RadioViaLocatorXPath(string name) => $"@type='radio' and @name='{name}'";
        private string WithId(string id) { return $@"@id = '{id}'"; }
        private string WithValue(string value) { return $@"@value = '{value}'"; }
        private string WithAncestorLabel(string text) { return $@"ancestor::label[contains(normalize-space(text()),'{text}')]"; }
        private string WithLabelFor(string id) { return $@"@id = //label[contains(normalize-space(),'{id}')]/@for"; }

        private ElementScope FindRadioButton(string value)
        {
            var xpath = $@"//input
                [
                    {RadioViaLocatorXPath(Locator)} and 
                    ({WithId(value)} or {WithValue(value)} or {WithAncestorLabel(value)} or {WithLabelFor(value)})
                ]";
            return Browser.FindXPath(xpath);
        }
    }
}
