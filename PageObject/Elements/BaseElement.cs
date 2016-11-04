using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Coypu;

namespace PageObject.Elements
{
    public abstract class BaseElement
    {
        protected BrowserSession Browser { get; }
        protected string Locator => Attribute.Locator;
        protected DriverScope SearchScope => Browser;

        protected ElementAttribute Attribute { get; }
        public virtual string FinderName => "";

        protected BaseElement(ElementAttribute attribute, BrowserSession browser)
        {
            Attribute = attribute;
            Browser = browser;
        }

        protected static string LabelTextFor(ElementScope element)
        {
            var allLabels = element.FindAllXPath($"//label[@for='{element.Id}'] | .//parent::label");
            return string.Join(" ", allLabels.Select(label => label.Text));
        }

        protected ElementScope FindXPath(string xPath, Options options = null)
        {
            return SearchScope.FindXPath(xPath, options);
        }

        protected string WithId(string id) { return $"@id = '{id}'"; }
        protected string WithValue(string value) { return $"@value = '{value}'"; }
        protected string WithAncestorLabel(string text) { return $"ancestor::label[{WithText(text)}]"; }
        protected string WithLabelFor(string id) { return $"@id = //label[contains(normalize-space(),'{id}')]/@for"; }
        protected string WithText(string text) { return $"contains(translate(normalize-space(), ' &#9;&#10;&#13', ''),'{text}')"; }

        protected IEnumerable<ElementScope> FindAllXPathOrThrow(string xPath, string elementDescription = "element")
        {
            var elements = SearchScope.FindAllXPath(xPath).ToList();
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