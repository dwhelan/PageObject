using System;
using NUnit.Framework;
using PageObject.Tests.PageConstructor;

namespace PageObject.Tests.PageAttributes
{
    [TestFixture]
    public class WithBasePage : BaseTest
    {
        [TestCase(typeof(BasePageOnly))]
        [TestCase(typeof(BasePageAndPath))]
        [TestCase(typeof(BasePageAndNullPath))]
        [TestCase(typeof(BasePageAndEmptyPath))]
        [TestCase(typeof(NullBasePageAndPath))]
        public void Should_support_a_base_page(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

            [PageAt(BaseTest.Url)]
            private class BasePage : Page
            {
                public BasePage() : base(null) { }
            }

            [PageAt(typeof(BasePage))]
            private class BasePageOnly : Page
            {
                public BasePageOnly() : base(null) { }
            }

            [PageAt(typeof(BasePage), Path)]
            private class BasePageAndPath : Page
            {
                public BasePageAndPath() : base(null) { }
            }

            [PageAt(typeof(BasePage), null)]
            private class BasePageAndNullPath : Page
            {
                public BasePageAndNullPath() : base(null) { }
            }

            [PageAt(typeof(BasePage), "")]
            private class BasePageAndEmptyPath : Page
            {
                public BasePageAndEmptyPath() : base(null) { }
            }

            [PageAt((Type)null, BaseTest.Url)]
            private class NullBasePageAndPath : Page
            {
                public NullBasePageAndPath() : base(null) { }
            }

        [TestCase(typeof(BaseThatIsNotAPage))]
        public void Should_ensure_that_base_is_a_Page_class(Type pageClass)
        {
            AssertPageCreationThrows(pageClass, @"base page for .* must be a subclass of PageObject.Page");
        }

            [PageAt(typeof(string), null)]
            private class BaseThatIsNotAPage : Page
            {
                public BaseThatIsNotAPage() : base(null) { }
            }

        [TestCase(typeof(BasePageInConstructor))]
        [TestCase(typeof(BaseUriInConstructor))]
        [TestCase(typeof(BaseUrlInConstructor))]
        public void Should_ensure_that_base_page_is_not_allowed_in_constructor(Type pageClass)
        {
            AssertPageCreationThrows(pageClass, @"Cannot specify a base page, Uri or Url in the constructor when you have included a base (page|uri) in the PageAt\(\) attribute");
        }

            [PageAt(typeof(BasePage))]
            private class BasePageInConstructor : Page
            {
                public BasePageInConstructor() : base(null, new BasePage()) { }
            }

            [PageAt(typeof(BasePage))]
            private class BaseUriInConstructor : Page
            {
                public BaseUriInConstructor() : base(null, new Uri("http://host")) { }
            }

            [PageAt(typeof(BasePage))]
            private class BaseUrlInConstructor : Page
            {
                public BaseUrlInConstructor() : base(null, "http://host") { }
            }

        [TestCase(typeof(SelfReferencingPage), @"Page .*SelfReferencingPage cannot have itself as a base page")]
        [TestCase(typeof(CircularReference1), @"Detected circular base page references with .*CircularReference1 and .*CircularReference1B")]
        [TestCase(typeof(CircularReference2), @"Detected circular base page references with .*CircularReference2 and .*CircularReference2C")]
        public void Should_detect_circular_references_in_base_pages(Type pageClass, string regEx)
        {
            AssertPageCreationThrows(pageClass, regEx);
        }

            [PageAt(typeof(SelfReferencingPage), null)]
            private class SelfReferencingPage : Page
            {
                public SelfReferencingPage() : base(null) { }
            }

            // Circular references: CircularReference1 => CircularReference1B => CircularReference1

            [PageAt(typeof(CircularReference1B), null)]
            private class CircularReference1 : Page
            {
                public CircularReference1() : base(null) { }
            }

            [PageAt(typeof(CircularReference1), null)]
            private class CircularReference1B : Page
            {
                public CircularReference1B() : base(null) { }
            }

            // Circular references: CircularReference2 => CircularReference2B => CircularReference2C => CircularReference2

            [PageAt(typeof(CircularReference2B), null)]
            private class CircularReference2 : Page
            {
                public CircularReference2() : base(null) { }
            }

            [PageAt(typeof(CircularReference2C), null)]
            private class CircularReference2B : Page
            {
                public CircularReference2B() : base(null) { }
            }

            [PageAt(typeof(CircularReference2), null)]
            private class CircularReference2C : Page
            {
                public CircularReference2C() : base(null) { }
            }
    }
}