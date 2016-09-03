using System;
using Coypu;
using NUnit.Framework;
using PageObject;

namespace PageObjectTests
{
    [TestFixture]
    public class PageTests
    {
        private PageSession session;

        [SetUp]
        public void CreateSession()
        {
            var configuration = new SessionConfiguration();
            session = new PageSession(configuration);
        }

        [Test]
        public void Should_support_file_scheme()
        {
            var page = new FileHomePage(session);
            page.Visit();
            Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }

        [Test]
        public void Should_support_Uri_in_constructor()
        {
            var page = new FileHomePageWithUri(session);
            page.Visit();
            Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }

        [TearDown]
        public void DisposeBrowser()
        {
            session?.Dispose();
        }
    }

    public class FileHomePage : Page
    {
        public FileHomePage(PageSession session) : base(session, "file:///Z:/code/cs/PageObject/PageObjectTests/FilePages/Home.html")
        {
        }
    }

    public class FileHomePageWithUri : Page
    {
        public FileHomePageWithUri(PageSession session) : base(session, new Uri("file:///Z:/code/cs/PageObject/PageObjectTests/FilePages/Home.html"))
        {
        }
    }
}
