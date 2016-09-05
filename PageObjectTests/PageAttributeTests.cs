using System;
using NUnit.Framework;
using PageObject;
using PageObjectTests.Pages;

namespace PageObjectTests
{
    [TestFixture]
    public class PageAttributeTests
    {
        [TestCase(typeof(WithPathOnly))]
        [TestCase(typeof(WithParentAndPath))]
        [TestCase(typeof(WithParentAndMissingPath))]
        [TestCase(typeof(WithParentAndNullPath))]
        [TestCase(typeof(WithParentAndEmptyPath))]
        public void Valid_page_classes(Type pageClass)
        {
            var page = ((Page) Activator.CreateInstance(pageClass));
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Constants.Url));
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
    }
}
