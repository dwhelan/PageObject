using System;
using NUnit.Framework;

namespace PageObject.Tests.PageAttributes
{
    [TestFixture]
    public class WithBasePage : BaseTest
    {
        [TestCase(typeof(BasePageAndPath))]
        [TestCase(typeof(BasePageAndNullPath))]
        [TestCase(typeof(BasePageAndEmptyPath))]
        [TestCase(typeof(BasePageOnly))]
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

            [PageAt(typeof(BasePage))]
            private class BasePageOnly : Page
            {
                public BasePageOnly() : base(null) { }
            }

            [PageAt((Type)null, BaseTest.Url)]
            private class NullBasePageAndPath : Page
            {
                public NullBasePageAndPath() : base(null) { }
            }

        [TestCase(typeof(BaseThatIsNotAPage))]
        public void Should_ensure_that_base_is_a_Page_class(Type pageClass)
        {
            AssertPageCreationThrowsPageObjectException(pageClass, @"base page for .* must be a subclass of PageObject.Page");
        }

            [PageAt(typeof(string), null)]
            private class BaseThatIsNotAPage : Page
            {
                public BaseThatIsNotAPage() : base(null) { }
            }

        [TestCase(typeof(SelfReferencingPage), @"Page .*SelfReferencingPage cannot have itself as a base page")]
        [TestCase(typeof(CircularReference1A), @"Detected circular base page references with .*CircularReference1A and .*CircularReference1B")]
        [TestCase(typeof(CircularReference2A), @"Detected circular base page references with .*CircularReference2A and .*CircularReference2C")]
        public void Should_detect_circular_references_in_base_pages(Type pageClass, string regEx)
        {
            AssertPageCreationThrowsPageObjectException(pageClass, regEx);
        }

            [PageAt(typeof(SelfReferencingPage), null)]
            private class SelfReferencingPage : Page
            {
                public SelfReferencingPage() : base(null) { }
            }

            // Circular references: CircularReference1a => CircularReference1b => CircularReference1a

            [PageAt(typeof(CircularReference1B), null)]
            private class CircularReference1A : Page
            {
                public CircularReference1A() : base(null) { }
            }

            [PageAt(typeof(CircularReference1A), null)]
            private class CircularReference1B : Page
            {
                public CircularReference1B() : base(null) { }
            }

            // Circular references: CircularReference2a => CircularReference2b => CircularReference2c => CircularReference2a

            [PageAt(typeof(CircularReference2B), null)]
            private class CircularReference2A : Page
            {
                public CircularReference2A() : base(null) { }
            }

            [PageAt(typeof(CircularReference2C), null)]
            private class CircularReference2B : Page
            {
                public CircularReference2B() : base(null) { }
            }

            [PageAt(typeof(CircularReference2A), null)]
            private class CircularReference2C : Page
            {
                public CircularReference2C() : base(null) { }
            }
    }
}