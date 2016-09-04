using System.Collections.Generic;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject;
using PageObjectTests.FilePages;

namespace PageObjectTests
{
    [TestFixture]
    public class PageConstructorTests
    {
        private PageSession session;

        [SetUp]
        public void CreateSession()
        {
            var configuration = new SessionConfiguration{ Browser = Browser.PhantomJS };
            session = new PageSession(configuration);
        }

        [Test]
        public void Should_support_uri_only()
        {
            EnsureHomePageIsValid(new HomePage(session, HomePage.Uri));
        }

        [Test]
        public void Should_support_uri_and_relative_path()
        {
            EnsureHomePageIsValid(new HomePage(session, Root.Uri, "Home.html"));
        }

        [Test]
        public void Should_support_url_only()
        {
            EnsureHomePageIsValid(new HomePage(session, HomePage.Url));
        }

        [Test]
        public void Should_support_url_and_relative_path()
        {
            EnsureHomePageIsValid(new HomePage(session, Root.Url, "Home.html"));
        }

        [TearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }

        private void EnsureHomePageIsValid(Page page)
        {
            Assert.That(page.Uri, Is.EqualTo(HomePage.Uri));
            Assert.That(page.Url, Is.EqualTo(HomePage.Url));
            CollectionAssert.AreEqual(page.Hosts, new List<string> { "" });

            page.Visit();
            Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }
    }
}