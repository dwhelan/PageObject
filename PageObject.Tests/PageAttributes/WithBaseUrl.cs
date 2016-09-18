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

            [PageAt(BaseTest.Url)]
            private class BaseUrlOnly : Page
            {
                public BaseUrlOnly(PageSession session = null) : base(session) { }
            }

            [PageAt(BaseUrl, Path)]
            private class BaseUrlAndPath : Page
            {
                public BaseUrlAndPath(PageSession session = null) : base(session) { }
            }

            [PageAt(BaseTest.Url, null)]
            private class BaseUrlAndNullPath : Page
            {
                public BaseUrlAndNullPath(PageSession session = null) : base(session) { }
            }

            [PageAt(BaseTest.Url, "")]
            private class BaseUrlAndEmptyPath : Page
            {
                public BaseUrlAndEmptyPath(PageSession session = null) : base(session) { }
            }

            [PageAt((string)null, BaseTest.Url)]
            private class NullBaseUrlAndPath : Page
            {
                public NullBaseUrlAndPath(PageSession session = null) : base(session) { }
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
