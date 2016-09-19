using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Tests.Pages.File;

namespace PageObject.Tests
{
    [TestFixture]
    public class OnPageTests
    {
        private SessionConfiguration configuration;
        private PageSession session;
        private Page page;

        [TestFixtureSetUp]
        public void VisitTheHomePage()
        {
            configuration = new SessionConfiguration { Browser = Browser.PhantomJS };
            session = new PageSession(configuration);
            page = new HomePage(session);
            page.Visit();
        }

        [Test]
        public void Page_should_be_active_when_visited()
        {
            Assert.That(page.IsActive);
        }

        [Test]
        public void Another_instance_of_the_page_should_be_active_when_visited()
        {
            var anotherPage = new HomePage(session);

            Assert.That(anotherPage.IsActive);
        }

        [Test]
        public void Another_page_with_the_same_url_should_be_active_when_visited()
        {
            var anotherPage = new HomePage2(session);
        
            Assert.That(anotherPage.IsActive);
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
