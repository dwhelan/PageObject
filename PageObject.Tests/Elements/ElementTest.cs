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
        protected TP Page;
        private PageSession session;

        protected TE Element => Page.Element;

        [TestFixtureSetUp]
        public void CreatePage()
        {
            CreateSession();
            Page = (TP) Activator.CreateInstance(typeof(TP), session);
            Page.Visit();
        }

        private void CreateSession()
        {
            var configuration = new SessionConfiguration {Browser = Browser.PhantomJS};
            session = new PageSession(configuration);
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
