using Coypu;
using Coypu.Drivers;
using NUnit.Framework;

namespace PageObject.Tests
{
    [TestFixture]
    public class PageSessionTests
    {
        private SessionConfiguration configuration;
        private PageSession session;

        [TestFixtureSetUp]
        public void CreateSession()
        {
            configuration = new SessionConfiguration { Browser = Browser.PhantomJS };
            session = new PageSession(configuration);
        }

        [Test]
        public void Should_expose_configuration()
        {
            Assert.That(session.Configuration, Is.SameAs(configuration));
        }

        [Test]
        public void Should_create_and_expose_browser()
        {
            Assert.That(session.Browser, Is.InstanceOf<BrowserSession>());
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
