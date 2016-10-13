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
            set { Driver.Choose(RadioButtonFor(value)); }
        }

        public IList<string> Options => RadioButtons.Select(radioButton => radioButton.Value).ToList();

        private IEnumerable<ElementScope> RadioButtons => FindOrThrow(RadioButtonsXpath());

        private ElementScope Selection => RadioButtons.FirstOrDefault(radioButton => radioButton.Selected);

        private IEnumerable<ElementScope> FindOrThrow(string xPath)
        {
            var scopes = Scope.FindAllXPath(xPath).ToList();
            if (scopes.Count == 0)
            {
                throw new MissingHtmlException($"Could not find radio button via xpath {xPath}");
            }
            return scopes;
        }

        private Scope Scope => Browser;

        private ElementScope RadioButtonFor(string value)
        {
            return Scope.FindXPath(RadioButtonsXpath($"and ({WithId(value)} or {WithValue(value)} or {WithAncestorLabel(value)} or {WithLabelFor(value)})"));
        }

        private string RadioButtonsXpath(string contraints = "")
        {
            return $".//input[{RadioViaLocator(Locator)} {contraints}]";
        }

        private string RadioViaLocator(string name) => $"@type='radio' and @name='{name}'";
        private string WithId(string id) { return $@"@id = '{id}'"; }
        private string WithValue(string value) { return $@"@value = '{value}'"; }
        private string WithAncestorLabel(string text) { return $@"ancestor::label[contains(normalize-space(text()),'{text}')]"; }
        private string WithLabelFor(string id) { return $@"@id = //label[contains(normalize-space(),'{id}')]/@for"; }
    }
}
