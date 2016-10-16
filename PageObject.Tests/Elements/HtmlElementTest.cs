using System;
using System.IO;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class HtmlElementTest<TP, TE> : ElementTest<TP, TE> where TP : TestPage<TE> where TE : HtmlElement
    {
        [Test]
        public virtual void Should_provide_text()
        {
            Assert.That(Element.Text, Is.EqualTo(Element.Base.Text));
        }

        [Test]
        public virtual void Base_should_provide_lower_level_access_to_the_page_element()
        {
            Assert.That(Element.Base.OuterHTML, Is.EqualTo(NormalizeHtml(ElementHtml)));
        }

        protected static string NormalizeHtml(string html)
        {
            return html.Trim().Replace("'", "\"");
        }
    }
}
