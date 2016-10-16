using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Coypu;

namespace PageObject.Elements
{
    public abstract class Element
    {
        protected BrowserSession Browser { get; }
        protected Driver Driver => Browser.Driver;
        protected string Locator => Attribute.Locator;

        protected Element(ElementAttribute attribute, BrowserSession browser)
        {
            Attribute = attribute;
            Browser = browser;
        }

        public Scope Scope => Browser;

        private ElementAttribute Attribute { get; }

        protected string LabelTextFor(ElementScope element)
        {
            var allLabels = element.FindAllXPath($"//label[@for='{element.Id}'] | .//parent::label");
            return string.Join(" ", allLabels.Select(label => label.Text));
        }

        protected ElementScope FindXPath(string xPath, Options options = null)
        {
            return Scope.FindXPath(xPath, options);
        }

        protected string WithId(string id) { return $"@id = '{id}'"; }
        protected string WithValue(string value) { return $"@value = '{value}'"; }
        protected string WithAncestorLabel(string text) { return $"ancestor::label[{WithText(text)}]"; }
        protected string WithLabelFor(string id) { return $"@id = //label[contains(normalize-space(),'{id}')]/@for"; }
        protected string WithText(string text) { return $"contains(translate(normalize-space(), ' &#9;&#10;&#13', ''),'{text}')"; }

        protected IEnumerable<ElementScope> FindAllXPathOrThrow(string xPath, string elementDescription = "element")
        {
            var elements = Scope.FindAllXPath(xPath).ToList();
            if (elements.Count == 0)
                throw new MissingHtmlException($"Could not find {elementDescription} via xpath {xPath}");

            return elements;
        }

        protected string StripWhitespace(string text)
        {
            return Regex.Replace(text, @"\s+", "");
        }
    }
}
