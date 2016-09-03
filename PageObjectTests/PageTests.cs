using System;
using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Watin;
using NUnit.Framework;
using PageObject;

namespace PageObjectTests
{
    [TestFixture]
    public class PageTests
    {
        private PageSession session;

        [Test]
        public void Default()
        {
        }

        [Test]
        public void Should_support_file_scheme()
        {
            var configuration = new SessionConfiguration();
            session = new PageSession(configuration);
            var page = new FileHomePage(session);
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
        public FileHomePage(PageSession browser) : base(browser, "file:///Z:/code/cs/PageObject/PageObjectTests/FilePages/Home.html")
        {
        }
    }
}
