using Coypu;
using Coypu.Drivers;
using NUnit.Framework;

namespace PageObject.Tests.Elements
{
    public abstract class ElementTest
    {
        protected internal const string ElementsFolder = "file:///${cd}/../../Elements/";

        protected Page Page;
        private PageSession session;

        [TestFixtureSetUp]
        public void CreatePage()
        {
            CreateSession();
            Page = CreatePage(session);
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

        protected abstract Page CreatePage(PageSession session);
    }
}
