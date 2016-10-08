using System;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;

namespace PageObject.Tests.Elements
{
    public class Foo
    {
        protected internal const string ElementsFolder = "file:///${cd}/../../Elements/";
    }

    public abstract class ElementTest<TP, TE> where TP : BasePage<TE> where TE : PageObject.Elements.Element
    {
        protected TE Element => Page.Element;

        protected TP Page;
        private PageSession session;

        [TestFixtureSetUp]
        public void VisitPage()
        {
            session = CreateSession();
            Page = CreatePage(session);
            Page.Visit();
        }

        private static PageSession CreateSession()
        {
            var configuration = new SessionConfiguration { Browser = Browser.PhantomJS };
            return new PageSession(configuration);
        }

        private static TP CreatePage(PageSession session)
        {
            return (TP) Activator.CreateInstance(typeof(TP), session);
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
