using System;
using NUnit.Framework;

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

            [PageAt(Tests.BaseTest.Url)]
            private class BasePage : Page
            {
                public BasePage(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(BasePage))]
            private class BasePageOnly : Page
            {
                public BasePageOnly(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(BasePage), Path)]
            private class BasePageAndPath : Page
            {
                public BasePageAndPath(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(BasePage), null)]
            private class BasePageAndNullPath : Page
            {
                public BasePageAndNullPath(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(BasePage), "")]
            private class BasePageAndEmptyPath : Page
            {
                public BasePageAndEmptyPath(PageSession session = null) : base(session) { }
            }

            [PageAt((Type)null, Tests.BaseTest.Url)]
            private class NullBasePageAndPath : Page
            {
                public NullBasePageAndPath(PageSession session = null) : base(session) { }
            }

        [TestCase(typeof(BaseThatIsNotAPage))]
        public void Should_ensure_that_base_is_a_Page_class(Type pageClass)
        {
            AssertInvokeThrows<PageObjectException>(() => CreatePage(pageClass), @"base page for .* must be a subclass of PageObject.Page");
        }

            [PageAt(typeof(string), null)]
            private class BaseThatIsNotAPage : Page
            {
                public BaseThatIsNotAPage(PageSession session = null) : base(session) { }
            }

        [TestCase(typeof(SelfReferencingPage), @"Page .*SelfReferencingPage cannot have itself as a base page")]
        [TestCase(typeof(CircularReference1), @"Detected circular base page references with .*CircularReference1 and .*CircularReference1B")]
        [TestCase(typeof(CircularReference2), @"Detected circular base page references with .*CircularReference2 and .*CircularReference2C")]
        public void Should_detect_circular_references_in_base_pages(Type pageClass, string regEx)
        {
            AssertInvokeThrows<PageObjectException>(() => CreatePage(pageClass), regEx);
        }

            [PageAt(typeof(SelfReferencingPage), null)]
            private class SelfReferencingPage : Page
            {
                public SelfReferencingPage(PageSession session = null) : base(session) { }
            }

            // Circular references: CircularReference1 => CircularReference1B => CircularReference1

            [PageAt(typeof(CircularReference1B), null)]
            private class CircularReference1 : Page
            {
                public CircularReference1(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(CircularReference1), null)]
            private class CircularReference1B : Page
            {
                public CircularReference1B(PageSession session = null) : base(session) { }
            }

            // Circular references: CircularReference2 => CircularReference2B => CircularReference2C => CircularReference2

            [PageAt(typeof(CircularReference2B), null)]
            private class CircularReference2 : Page
            {
                public CircularReference2(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(CircularReference2C), null)]
            private class CircularReference2B : Page
            {
                public CircularReference2B(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(CircularReference2), null)]
            private class CircularReference2C : Page
            {
                public CircularReference2C(PageSession session = null) : base(session) { }
            }
    }
}