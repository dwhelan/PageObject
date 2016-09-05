using System;
using System.Collections.Generic;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject;
using PageObjectTests.Pages.Google;
using WatiN.Core.Exceptions;

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
        public void Should_support_parent_and_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentAndPath());
        }


        [Test]
        public void Should_support_parent_and_missing_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentAndMissingPath());
        }

        [Test]
        public void Should_support_parent_and_empty_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentAndEmptyPath());
        }

        [Test]
        public void Should_support_parent_and_null_path()
        {
            EnsureHomeServicesPageIsValid(new ServicesPageWithParentAndNullPath());
        }

        [Test]
        public void Should_throw_if_parent_is_not_a_subclass_of_Page()
        {
            Assert.Throws<ArgumentException>(() => new ServicesPageWithParentThatIsNotAPage());
        }

        [Test, Ignore]
        public void Should_throw_if_parent_causes_circular_loop()
        {
            Assert.Fail("Not yet implemented");
        }

        [Test, Ignore]
        public void Should_support_uri_and_relative_path()
        {
            Assert.Fail("Not yet implemented");
        }

        [Test, Ignore]
        public void Should_support_url_only()
        {
            Assert.Fail("Not yet implemented");
        }

        [Test, Ignore]
        public void Should_support_url_and_relative_path()
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
