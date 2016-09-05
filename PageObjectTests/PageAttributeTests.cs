using System.Collections.Generic;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject;
using PageObjectTests.Pages.Google;

namespace PageObjectTests
{
    [TestFixture]
    public class PageAttributeTests
    {
        [Test]
        public void Should_support_uri_only()
        {
            EnsureHomePageIsValid(new HomePage(null));
        }

        [Test]
        public void Should_support_uri_and_relative_path()
        {
            //EnsureHomePageIsValid(new HomePage(session, Root.Uri, "Home.html"));
        }

        [Test]
        public void Should_support_url_only()
        {
            //EnsureHomePageIsValid(new HomePage(session, HomePage.Url));
        }

        [Test]
        public void Should_support_url_and_relative_path()
        {
            //EnsureHomePageIsValid(new HomePage(session, Root.Url, "Home.html"));
        }

        private void EnsureHomePageIsValid(Page page)
        {
            //Assert.That(page.Uri, Is.EqualTo(HomePage.Uri));
            Assert.That(page.Url, Is.EqualTo("http://www.google.com/"));
            //CollectionAssert.AreEqual(page.Hosts, new List<string> { "" });

            //page.Visit();
            //Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }
    }
}
