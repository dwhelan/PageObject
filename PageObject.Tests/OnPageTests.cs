using System;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;

namespace PageObject.Tests
{
    [PageAt("file:///${cd}/../../Pages/File/Home.html")]
    internal class HomePage : Page
    {
        public HomePage(PageSession session) : base(session) {}
    }

    [TestFixture]
    public class OnPageTests
    {
        private PageSession session;

        [TestFixtureSetUp]
        public void VisitTheHomePage()
        {
            var configuration = new SessionConfiguration { Browser = Browser.PhantomJS };
            session = new PageSession(configuration);
            new HomePage(session).Visit();
        }

        [TestCase(typeof(HomePage))]
        [TestCase(typeof(PageWithSameUrl))]
        [TestCase(typeof(PageWithMatchingHostAndSamePath))]
        [TestCase(typeof(PageWithSameHostAndMatchingPath))]
        public void Should_be_on_matching_pages(Type pageClass)
        {
            var anotherPage = PageFactory.Instance.PageFor(pageClass, session);

            Assert.That(anotherPage.IsActive);
        }

            [PageAt("file:///${cd}/../../Pages/File/Home.html")]
            internal class PageWithSameUrl : Page
            {
                public PageWithSameUrl(PageSession session) : base(session) { }
            }

            [PageAt("file://localhost/${cd}/../../Pages/File/Home.html", HostMatch = ".*")]
            internal class PageWithMatchingHostAndSamePath : Page
            {
                public PageWithMatchingHostAndSamePath(PageSession session) : base(session) { }
            }

            [PageAt("file:///${cd}/../../Pages/File2/Home.html", PathMatch = @"Z:/code/cs/PageObject/PageObject.Tests/Pages/File\d?/Home.html")]
            internal class PageWithSameHostAndMatchingPath : Page
            {
                public PageWithSameHostAndMatchingPath(PageSession session) : base(session)
                {
                }
            }
        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
