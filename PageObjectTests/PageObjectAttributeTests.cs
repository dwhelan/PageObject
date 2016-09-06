using System;
using System.Reflection;
using NUnit.Framework;
using PageObject;
using PageObjectTests.Pages;

namespace PageObjectTests
{
    [TestFixture]
    public class PageObjectAttributeTests
    {
        [TestCase(typeof(WithPathOnly))]
        public void Should_support_pages_with_a_url_only(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(WithPathAndNullBasePage))]
        [TestCase(typeof(WithPathAndBasePage))]
        [TestCase(typeof(WithNullPathAndBasePage))]
        [TestCase(typeof(WithEmptyPathAndBasePage))]
        public void Should_support_pages_with_a_base_page(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(WithPathAndNullBaseUrl))]
        [TestCase(typeof(WithPathAndBaseUrl))]
        [TestCase(typeof(WithNullPathAndBaseUrl))]
        [TestCase(typeof(WithEmptyPathAndBaseUrl))]
        public void Should_support_pages_with_a_base_url(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(WithInvalidUrl),             @"Invalid url ""invalid url""", typeof(UriFormatException))]
        [TestCase(typeof(WithBaseThatIsNotAPage),     @"base page for .* must be a subclass of PageObject.Page", null)]
        [TestCase(typeof(WithBaseThatIsNotAValidUrl), @"Invalid url ""invalid url""", typeof(UriFormatException))]
        public void Should_not_be_a_valid_page(Type pageClass, string regEx, Type nestedException)
        {
            var x = Assert.Throws<PageObjectException>(() => CreatePage(pageClass));
            StringAssert.IsMatch(regEx, x.Message);
            if (nestedException != null)
                Assert.That(x.InnerException, Is.AssignableTo(nestedException));
        }

        [Test, Ignore]
        public void Should_throw_if_parent_causes_circular_loop()
        {
            Assert.Fail("Not yet implemented");
        }

        private void AssertThatPageCanBeCreated(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Constants.Url));
        }

        private Page CreatePage(Type pageClass)
        {
            try
            {
                return (Page) Activator.CreateInstance(pageClass);
            }
            catch (TargetInvocationException x)
            {
                throw x.InnerException;
            }
        }
    }
}
