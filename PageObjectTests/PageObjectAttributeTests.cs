using System;
using System.Reflection;
using NUnit.Framework;
using PageObject;
using PageObjectTests.Pages.PageObjectAttributeTest;

namespace PageObjectTests
{
    [TestFixture]
    public class PageObjectAttributeTests
    {
        [TestCase(typeof(BasePage))]
        public void Should_support_pages_with_a_url_only(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(BasePageAndPath))]
        [TestCase(typeof(BasePageAndNullPath))]
        [TestCase(typeof(BasePageAndEmptyPath))]
        [TestCase(typeof(BasePageOnly))]
        [TestCase(typeof(NullBasePageAndPath))]
        public void Should_support_pages_with_a_base_page(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(BaseUrlAndPath))]
        [TestCase(typeof(BaseUrlAndNullPath))]
        [TestCase(typeof(BaseUrlAndEmptyPath))]
        [TestCase(typeof(NullBaseUrlAndPath))]
        public void Should_support_pages_with_a_base_url(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(BaseThatIsNotAPage))]
        public void Should_ensure_that_base_is_a_Page_class(Type pageClass)
        {
            AssertPageCreationThrowsPageObjectException(pageClass, @"base page for .* must be a subclass of PageObject.Page");
        }

        [TestCase(typeof(InvalidUrl))]
        [TestCase(typeof(BaseThatIsAnInvalidUrl))]
        public void Should_ensure_a_valid_uri(Type pageClass)
        {
            var x = AssertPageCreationThrowsPageObjectException(pageClass, @"Invalid url ""invalid url""");
            Assert.That(x.InnerException, Is.AssignableTo(typeof(UriFormatException)));
        }

        [TestCase(typeof(SelfReferencingPage),  @"Page .*SelfReferencingPage cannot have itself as a base page")]
        [TestCase(typeof(CircularReference1A),  @"Detected circular base page references with .*CircularReference1A and .*CircularReference1B")]
        [TestCase(typeof(CircularReference2A),  @"Detected circular base page references with .*CircularReference2A and .*CircularReference2C")]
        public void Should_detect_circular_references_in_base_pages(Type pageClass, string regEx)
        {
            AssertPageCreationThrowsPageObjectException(pageClass, regEx);
        }

        private Exception AssertPageCreationThrowsPageObjectException(Type pageClass, string regEx)
        {
            var x = Assert.Throws<PageObjectException>(() => CreatePage(pageClass));
            StringAssert.IsMatch(regEx, x.Message);
            return x;
        }

        private void AssertThatPageCanBeCreated(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Constants.Url));
        }

        private static Page CreatePage(Type pageClass)
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
