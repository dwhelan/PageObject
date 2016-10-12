using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Coypu;

namespace PageObject.Elements
{
    public class Radio : InputElement
    {
        public Radio(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser) { }

        public string Value
        {
            get { return Selection == null ? "" : Selection.Value; }
            set { Browser.Driver.Choose(FindRadioButton(value)); }
        }

        public IList<string> Options => FindAllRadioButtons().Select(radioButton => radioButton.Value).ToList();

        private SnapshotElementScope Selection => FindAllRadioButtons().FirstOrDefault(r => r.Selected);

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
