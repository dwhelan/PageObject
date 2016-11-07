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

        protected ElementScope FindXPath(string xPath, DriverScope scope, Options options = null)
        {
            return scope.FindXPath(xPath, options);
        }

        protected static string WithText(string text) { return $"contains(translate(normalize-space(), ' &#9;&#10;&#13', ''),'{text}')"; }

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
