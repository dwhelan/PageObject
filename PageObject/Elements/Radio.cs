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
            get { return Selection == null ? "" : Selection.Value; }
            set { Browser.Driver.Choose(RadioButtonFor(value)); }
        }

        public IList<string> Options => RadioButtons.Select(radioButton => radioButton.Value).ToList();

        private SnapshotElementScope Selection => RadioButtons.FirstOrDefault(r => r.Selected);

        private IEnumerable<SnapshotElementScope> RadioButtons
        {
            get
            {
                var scopes = Browser.FindAllXPath(RadioButtonXpath()).ToList();
                if (scopes.Count == 0)
                {
                    Browser.FindXPath(RadioButtonXpath()).HasValue("");
                }
                return scopes;
            }
        }

        private ElementScope RadioButtonFor(string value)
        {
            return Browser.FindXPath(RadioButtonXpath($"and ({WithId(value)} or {WithValue(value)} or {WithAncestorLabel(value)} or {WithLabelFor(value)})"));
        }

        private string RadioButtonXpath(string contraints = "")
        {
            return $"//input[{RadioViaLocator(Locator)} {contraints}]";
        }

        private string RadioViaLocator(string name) => $"@type='radio' and @name='{name}'";
        private string WithId(string id) { return $@"@id = '{id}'"; }
        private string WithValue(string value) { return $@"@value = '{value}'"; }
        private string WithAncestorLabel(string text) { return $@"ancestor::label[contains(normalize-space(text()),'{text}')]"; }
        private string WithLabelFor(string id) { return $@"@id = //label[contains(normalize-space(),'{id}')]/@for"; }
    }
}
