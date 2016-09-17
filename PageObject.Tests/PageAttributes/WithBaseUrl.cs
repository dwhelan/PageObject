using System;
using NUnit.Framework;

namespace PageObject.Tests.PageAttributes
{
    [TestFixture]
    public class WithBaseUrl : BaseTest
    {
        [TestCase(typeof(BaseUrlOnly))]
        [TestCase(typeof(BaseUrlAndPath))]
        [TestCase(typeof(BaseUrlAndNullPath))]
        [TestCase(typeof(BaseUrlAndEmptyPath))]
        [TestCase(typeof(NullBaseUrlAndPath))]
        public void Should_support_a_base_url(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

            [PageAt(Tests.BaseTest.Url)]
            private class BaseUrlOnly : Page
            {
                public BaseUrlOnly(PageSession session = null) : base(session) { }
            }

            [PageAt(BaseUrl, Path)]
            private class BaseUrlAndPath : Page
            {
                public BaseUrlAndPath(PageSession session = null) : base(session) { }
            }

            [PageAt(Tests.BaseTest.Url, null)]
            private class BaseUrlAndNullPath : Page
            {
                public BaseUrlAndNullPath(PageSession session = null) : base(session) { }
            }

            [PageAt(Tests.BaseTest.Url, "")]
            private class BaseUrlAndEmptyPath : Page
            {
                public BaseUrlAndEmptyPath(PageSession session = null) : base(session) { }
            }

            [PageAt((string)null, Tests.BaseTest.Url)]
            private class NullBaseUrlAndPath : Page
            {
                public NullBaseUrlAndPath(PageSession session = null) : base(session) { }
            }

        [Test]
        public void Should_allow_a_relative_path_in_the_constructor()
        {
            AssertThatPageCanBeCreated(typeof(PathInConstructor));
        }
            [PageAt(typeof(BasePage))]
            private class PathInConstructor : Page
            {
                public PathInConstructor(PageSession session = null) : base(session, Path) { }
            }

        [TestCase(typeof(BaseUriInConstructor))]
        [TestCase(typeof(BaseUrlInConstructor))]
        public void Should_ensure_that_base_page_is_not_allowed_in_constructor(Type pageClass)
        {
            AssertInvokeThrows<PageObjectException>(() => CreatePage(pageClass), @"Cannot specify a base Page, Uri or url in the constructor when you have included a base url in the PageAt\(\) attribute");
        }

            [PageAt(Tests.BaseTest.Url)]
            private class BasePage : Page
            {
                public BasePage(PageSession session = null) : base(session) {}
            }

            [PageAt(Tests.BaseTest.Url, "")]
            private class BaseUriInConstructor : Page
            {
                public BaseUriInConstructor(PageSession session = null) : base(session, Tests.BaseTest.Uri) {}
            }

            [PageAt(Tests.BaseTest.Url, "")]
            private class BaseUrlInConstructor : Page
            {
                public BaseUrlInConstructor(PageSession session = null) : base(session, Tests.BaseTest.Url) {}
            }

        [TestCase(typeof(InvalidUrl))]
        [TestCase(typeof(BaseThatIsAnInvalidUrl))]
        public void Should_ensure_a_valid_uri(Type pageClass)
        {
            AssertInvokeThrows<PageObjectException>(() => CreatePage(pageClass), @"Invalid url ""invalid url""");
        }

            [PageAt("invalid url")]
            private class InvalidUrl : Page
            {
                public InvalidUrl(PageSession session = null) : base(session ) { }
            }

            [PageAt("invalid url", "path")]
            private class BaseThatIsAnInvalidUrl : Page
            {
                public BaseThatIsAnInvalidUrl(PageSession session = null) : base(session) { }
            }
    }
}
