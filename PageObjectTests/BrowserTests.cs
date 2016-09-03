using System;
using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Watin;
using NUnit.Framework;

namespace PageObjectTests
{
    [TestFixture]
    public class BrowserTests
    {
        private BrowserSession browserSession;

        [Test]
        public void Default()
        {
            TestGoogleSearch();
        }

        [Test]
        public void Firefox35()
        {
            TestGoogleSearch(Browser.Firefox);
        }

        [Test]
        public void InternetExplorer()
        {
            TestGoogleSearch(Browser.InternetExplorer);
        }

        [Test, RequiresSTA]
        public void InternetExplorer_with_WatiN()
        {
            TestGoogleSearch(Browser.InternetExplorer, typeof(WatiNDriver));
        }

        [Test]
        public void Chrome()
        {
            TestGoogleSearch(Browser.Chrome);
        }

        [Test]
        public void Phantomjs()
        {
            TestGoogleSearch(Browser.PhantomJS);
        }

        private void TestGoogleSearch(Browser browser = null, Type driver = null)
        {
            var configuration = new SessionConfiguration { AppHost = "google.com" };

            if (browser != null) { configuration.Browser = browser; }
            if (driver != null) { configuration.Driver = driver; }
            browserSession = new BrowserSession(configuration);

            browserSession.Visit("/");

            Assert.That(browserSession.Title, Is.EqualTo("Google"));
        }

        [TearDown]
        public void DisposeBrowser()
        {
            browserSession.Dispose();
        }
    }
}
