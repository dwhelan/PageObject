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
                public BaseUrlOnly() : base(null) { }
            }

            [PageAt(BaseUrl, Path)]
            private class BaseUrlAndPath : Page
            {
                public BaseUrlAndPath() : base(null) { }
            }

            [PageAt(BaseTest.Url, null)]
            private class BaseUrlAndNullPath : Page
            {
                public BaseUrlAndNullPath() : base(null) { }
            }

            [PageAt(BaseTest.Url, "")]
            private class BaseUrlAndEmptyPath : Page
            {
                public BaseUrlAndEmptyPath() : base(null) { }
            }

            [PageAt((string)null, BaseTest.Url)]
            private class NullBaseUrlAndPath : Page
            {
                public NullBaseUrlAndPath() : base(null) { }
            }

        [TestCase(typeof(InvalidUrl))]
        [TestCase(typeof(BaseThatIsAnInvalidUrl))]
        public void Should_ensure_a_valid_uri(Type pageClass)
        {
            var x = AssertPageCreationThrows(pageClass, @"Invalid url ""invalid url""");
            Assert.That(x.InnerException, Is.AssignableTo(typeof(UriFormatException)));
        }

            [PageAt("invalid url")]
            private class InvalidUrl : Page
            {
                public InvalidUrl() : base(null) { }
            }

            [PageAt("invalid url", "path")]
            private class BaseThatIsAnInvalidUrl : Page
            {
                public BaseThatIsAnInvalidUrl() : base(null) { }
            }
    }
}
