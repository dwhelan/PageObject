using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class HtmlElementTest<TP, TE> : ElementTest<TP, TE> where TP : TestPage<TE> where TE : HtmlElement
    {
        private HtmlElement HtmlElement => Element;

        [Test]
        public virtual void Should_provide_text()
        {
            Assert.That(HtmlElement.Text, Is.EqualTo(HtmlElement.Element.Text));
        }

        [Test]
        public virtual void Should_provide_title()
        {
            Assert.That(HtmlElement.Title, Is.EqualTo(HtmlElement.Element.Title));
        }

        [Test]
        public virtual void Should_provide_lower_level_access_to_the_page_element()
        {
            Assert.That(HtmlElement.Element.OuterHTML, Is.EqualTo(NormalizeHtml(ElementHtml)));
        }

        protected static string NormalizeHtml(string html)
        {
            return html.Trim().Replace("'", "\"");
        }
    }
}
