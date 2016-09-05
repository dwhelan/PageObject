using System;
using NUnit.Framework;
using PageObject;
using PageObjectTests.Pages.AttributeTests;

namespace PageObjectTests
{
    [TestFixture]
    public class PageAttributeTests
    {
        [Test]
        public void Should_support_path_only()
        {
            EnsurePageIsValid(new WithPathOnly());
        }

        [Test]
        public void Should_support_parent_and_path()
        {
            EnsurePageIsValid(new WithParentAndPath());
        }

        [Test]
        public void Should_support_parent_and_missing_path()
        {
            EnsurePageIsValid(new WithParentAndMissingPath());
        }

        [Test]
        public void Should_support_parent_and_empty_path()
        {
            EnsurePageIsValid(new WithParentAndEmptyPath());
        }

        [Test]
        public void Should_support_parent_and_null_path()
        {
            EnsurePageIsValid(new WithParentAndNullPath());
        }

        [Test]
        public void Should_throw_if_parent_is_not_a_subclass_of_Page()
        {
            Assert.Throws<ArgumentException>(() => new WithParentThatIsNotAPage());
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

        private void EnsurePageIsValid(Page page)
        {
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Constants.Url));
            //CollectionAssert.AreEqual(page.Hosts, new List<string> { "" });

            //page.Visit();
            //Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }

    }
}
