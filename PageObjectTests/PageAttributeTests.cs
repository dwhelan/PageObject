using System;
using System.Reflection;
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
        public void Should_be_a_valid_page(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Constants.Url));
        }

        [TestCase(typeof(WithParentThatIsNotAPage))]
        public void Should_not_be_a_valid_page(Type pageClass)
        {
            Assert.Throws<ArgumentException>(() => CreatePage(pageClass));
        }

        [Test, Ignore]
        public void Should_throw_if_parent_causes_circular_loop()
        {
            Assert.Fail("Not yet implemented");
        }

        [Test, Ignore]
        public void Should_support_uri_only()
        {
            Assert.Fail("Not yet implemented");
        }

        [Test, Ignore]
        public void Should_support_uri_and_relative_path()
        {
            Assert.Fail("Not yet implemented");
        }

        private static Page CreatePage(Type pageClass)
        {
            try
            {
                return (Page)Activator.CreateInstance(pageClass);
            }
            catch (TargetInvocationException x)
            {
                throw x.InnerException;
            }
        }
    }
}
