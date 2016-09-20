using System;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;

namespace PageObject.Tests
{
    [PageAt("file:///{cd}/../../Pages/File/Home.html")]
    internal class HomePage : Page
    {
        public HomePage(PageSession session) : base(session) {}
    }

    [PageAt("file:///{cd}/../../Pages/File/Home.html")]
    internal class HomePage2 : Page
    {
        public HomePage2(PageSession session) : base(session) { }
    }

    [PageAt("file://localhost/{cd}/../../Pages/File/Home.html", HostMatch = ".*")]
    internal class HomePage3 : Page
    {
        public HomePage3(PageSession session) : base(session) { }
    }

    [PageAt("file:///{cd}/../../Pages/File2/Home.html", PathMatch = @"Z:/code/cs/PageObject/PageObject.Tests/Pages/File\d?/Home.html")]
    internal class HomePage4 : Page
    {
        public HomePage4(PageSession session) : base(session)
        {
        }
    }

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

        [Test]
        public void Another_page_with_same_path_and_matching_host_should_be_active()
        {
            var anotherPage = new HomePage3(session);
        
            Assert.That(anotherPage.IsActive);
        }

        [Test]
        public void Another_page_with_a_matching_path_should_be_active()
        {
            var anotherPage = new HomePage4(session);
        
            Assert.That(anotherPage.IsActive);
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
