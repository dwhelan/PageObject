using System;
using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Watin;
using NUnit.Framework;

namespace PageObject.Tests
{
    // These tests are intended to help people see what browsers are supported on their local machine. As a result they are tagged as explicit.

    [TestFixture, Explicit]
    public class BrowserTests
    {
        private BrowserSession browserSession;

        [Test]
        public void Default()
        {
            TestBrowserInterationWith();
        }

        [Test]
        public void Firefox35()
        {
            TestBrowserInterationWith(Browser.Firefox);
        }

        [Test]
        public void InternetExplorer()
        {
            TestBrowserInterationWith(Browser.InternetExplorer);
        }

        [Test, RequiresSTA]
        public void InternetExplorer_with_WatiN()
        {
            TestBrowserInterationWith(Browser.InternetExplorer, typeof(WatiNDriver));
        }

        [Test]
        public void Chrome()
        {
            TestBrowserInterationWith(Browser.Chrome);
        }

        [Test]
        public void PhantomJs()
        {
            TestBrowserInterationWith(Browser.PhantomJS);
        }

        private void TestBrowserInterationWith(Browser browser = null, Type driver = null)
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
