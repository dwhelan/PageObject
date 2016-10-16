using System;
using System.IO;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public static class TestConstants
    {
        internal const string HtmlFileName = "ElementTestPage.html";
    }

    public abstract class ElementTest<TP, TE> where TP : TestPage<TE> where TE : HtmlElement
    {
        protected virtual Browser Browser => Browser.PhantomJS;

        protected TE Element => page.Element;
        protected abstract string ElementHtml { get; }

        protected PageSession Session { get; private set; }
        private TP page;

        [TestFixtureSetUp]
        public void CreatePage()
        {
            Session = CreateSession();
            page = CreatePage(Session);
            WriteHtml();
        }

        [SetUp]
        public void VisitPage()
        {
            page.Visit();
        }

        private void WriteHtml()
        {
            using (var writer = new StreamWriter(TestConstants.HtmlFileName))
            {
                writer.WriteLine(ElementHtml);
            }
        }

        private PageSession CreateSession()
        {
            var configuration = new SessionConfiguration { Browser = Browser };
            return new PageSession(configuration);
        }

        private static TP CreatePage(PageSession session)
        {
            return (TP) Activator.CreateInstance(typeof(TP), session);
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            Session?.Dispose();
        }

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
