using System;
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
        public void Should_support_path_only()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithPathOnly());
        }

        [Test]
        public void Should_support_parent_page_and_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentPageAndPath());
        }


        [Test]
        public void Should_support_parent_page_and_missing_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentPageAndMissingPath());
        }

        [Test]
        public void Should_support_parent_page_and_empty_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentPageAndEmptyPath());
        }

        [Test]
        public void Should_support_parent_page_and_null_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentPageAndNullPath());
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

        [Test, Ignore]
        public void Should_throw_if_page_class_is_not_a_subclass_of_Page()
        {
            Assert.Fail("Not yet implemented");
        }

        [Test, Ignore]
        public void Should_throw_if_page_class_causes_circular_loop()
        {
            Assert.Fail("Not yet implemented");
        }

        private void EnsureHomeServicesPageIsValid(Page page)
        {
            Assert.That(page.Uri, Is.EqualTo(ServicesPage.Uri));
            Assert.That(page.Url, Is.EqualTo("http://www.google.com/services"));
            //CollectionAssert.AreEqual(page.Hosts, new List<string> { "" });

            //page.Visit();
            //Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }

    }
}
