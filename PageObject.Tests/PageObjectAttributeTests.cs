using System;
using System.Reflection;
using NUnit.Framework;

namespace PageObject.Tests
{
    public static class Constants
    {
        public const string Url = BaseUrl + Path;
        public const string BaseUrl = "file:///";
        public const string Path = "something";
    }

    // The following pages classes should all be valid with a Uri.AbsoluteUri equal to Constants.Url

    [PageAt(Constants.Url)]
    public class BasePage : Page
    {
        public BasePage() : base(null) { }
    }


    // Valid page objects built with base page objects.

    [PageAt(typeof(BasePage), Constants.Path)]
    public class BasePageAndPath : Page
    {
        public BasePageAndPath() : base(null) { }
    }

    [PageAt(typeof(BasePage), null)]
    public class BasePageAndNullPath : Page
    {
        public BasePageAndNullPath() : base(null) { }
    }

    [PageAt(typeof(BasePage), "")]
    public class BasePageAndEmptyPath : Page
    {
        public BasePageAndEmptyPath() : base(null) { }
    }

    [PageAt(typeof(BasePage))]
    public class BasePageOnly : Page
    {
        public BasePageOnly() : base(null) { }
    }

    [PageAt((Type)null, Constants.Url)]
    public class NullBasePageAndPath : Page
    {
        public NullBasePageAndPath() : base(null) { }
    }


    // Valid page objects with base urls.

    [PageAt(Constants.BaseUrl, Constants.Path)]
    public class BaseUrlAndPath : Page
    {
        public BaseUrlAndPath() : base(null) { }
    }

    [PageAt(Constants.Url, null)]
    public class BaseUrlAndNullPath : Page
    {
        public BaseUrlAndNullPath() : base(null) { }
    }

    [PageAt(Constants.Url, "")]
    public class BaseUrlAndEmptyPath : Page
    {
        public BaseUrlAndEmptyPath() : base(null) { }
    }

    [PageAt((string)null, Constants.Url)]
    public class NullBaseUrlAndPath : Page
    {
        public NullBaseUrlAndPath() : base(null) { }
    }


    // The following invalid page classes should raise a PageObjectException when instantiated.

    [PageAt("invalid url")]
    public class InvalidUrl : Page
    {
        public InvalidUrl() : base(null) { }
    }

    [PageAt(typeof(string), null)]
    public class BaseThatIsNotAPage : Page
    {
        public BaseThatIsNotAPage() : base(null) { }
    }

    [PageAt("invalid url", "path")]
    public class BaseThatIsAnInvalidUrl : Page
    {
        public BaseThatIsAnInvalidUrl() : base(null) { }
    }

    [PageAt(typeof(SelfReferencingPage), null)]
    public class SelfReferencingPage : Page
    {
        public SelfReferencingPage() : base(null) { }
    }

    // Circular references: CircularReference1a => CircularReference1b => CircularReference1a

    [PageAt(typeof(CircularReference1B), null)]
    public class CircularReference1A : Page
    {
        public CircularReference1A() : base(null) { }
    }

    [PageAt(typeof(CircularReference1A), null)]
    public class CircularReference1B : Page
    {
        public CircularReference1B() : base(null) { }
    }

    // Circular references: CircularReference2a => CircularReference2b => CircularReference2c => CircularReference2a

    [PageAt(typeof(CircularReference2B), null)]
    public class CircularReference2A : Page
    {
        public CircularReference2A() : base(null) { }
    }

    [PageAt(typeof(CircularReference2C), null)]
    public class CircularReference2B : Page
    {
        public CircularReference2B() : base(null) { }
    }

    [PageAt(typeof(CircularReference2A), null)]
    public class CircularReference2C : Page
    {
        public CircularReference2C() : base(null) { }
    }
    [TestFixture]
    public class PageObjectAttributeTests
    {
        [TestCase(typeof(BasePage))]
        public void Should_support_url_only(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(BasePageAndPath))]
        [TestCase(typeof(BasePageAndNullPath))]
        [TestCase(typeof(BasePageAndEmptyPath))]
        [TestCase(typeof(BasePageOnly))]
        [TestCase(typeof(NullBasePageAndPath))]
        public void Should_support_a_base_page(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

        [TestCase(typeof(BaseUrlAndPath))]
        [TestCase(typeof(BaseUrlAndNullPath))]
        [TestCase(typeof(BaseUrlAndEmptyPath))]
        [TestCase(typeof(NullBaseUrlAndPath))]
        public void Should_support_a_base_url(Type pageClass)
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
