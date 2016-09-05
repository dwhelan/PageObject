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
            EnsureHomeServicesPageIsValid(new ServicesPageWithUrlOnly());
        }

        [Test, Ignore]
        public void Should_support_parent_page_and_relative_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPage());
        }

        [Test]
        public void Should_support_uri_and_relative_path()
        {
        }

        [Test]
        public void Should_support_url_only()
        {
        }

        [Test]
        public void Should_support_url_and_relative_path()
        {
        }

        private void EnsureHomeServicesPageIsValid(Page page)
        {
            //Assert.That(page.Uri, Is.EqualTo(HomePage.Uri));
            Assert.That(page.Url, Is.EqualTo("http://www.google.com/services"));
            //CollectionAssert.AreEqual(page.Hosts, new List<string> { "" });

            //page.Visit();
            //Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }
    }
}
