using System.Collections.Generic;
using System.Linq;
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

        private ElementAttribute Attribute { get; }
        protected Scope Scope => Browser;

        protected ElementScope FindXPath(string xPath)
        {
            return Scope.FindXPath(xPath);
        }

        protected string WithId(string id)              { return $"@id = '{id}'"; }
        protected string WithValue(string value)        { return $"@value = '{value}'"; }
        protected string WithAncestorLabel(string text) { return $"ancestor::label[contains(normalize-space(text()),'{text}')]"; }
        protected string WithLabelFor(string id)        { return $"@id = //label[contains(normalize-space(),'{id}')]/@for"; }

        protected IEnumerable<ElementScope> FindAllXPathOrThrow(string xPath, string elementDescription="element")
        {
            var elements = Scope.FindAllXPath(xPath).ToList();
            if (elements.Count == 0)
                throw new MissingHtmlException($"Could not find {elementDescription} via xpath {xPath}");

            return elements;
        }
    }
}
