using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstruction
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

            [PageAt((Type)null, BaseTest.Url)]
            private class NullBasePageAndPath : Page
            {
                public NullBasePageAndPath(PageSession session = null) : base(session) { }
            }

        [Test]
        public void Should_ensure_that_base_is_a_Page_class()
        {
            AssertInvokeThrows<PageObjectException>(() => CreatePage(typeof(BaseThatIsNotAPage)), @"base page for .*BaseThatIsNotAPage must be a subclass of PageObject.Page");
        }

            [PageAt(typeof(string), null)]
            private class BaseThatIsNotAPage : Page
            {
                public BaseThatIsNotAPage(PageSession session = null) : base(session) { }
            }

        [TestCase(typeof(SelfReferencingPage), @"Detected circular base page references with .*SelfReferencingPage => .*SelfReferencingPage")]
        [TestCase(typeof(CircularReference1), @"Detected circular base page references with .*CircularReference1 => .*CircularReference1A => .*CircularReference1")]
        [TestCase(typeof(CircularReference2), @"Detected circular base page references with .*CircularReference2 => .*CircularReference2A => .*CircularReference2B => .*CircularReference2")]
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

            [PageAt(typeof(CircularReference1A), null)]
            private class CircularReference1 : Page
            {
                public CircularReference1(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(CircularReference1), null)]
            private class CircularReference1A : Page
            {
                public CircularReference1A(PageSession session = null) : base(session) { }
            }

            // Circular references: CircularReference2 => CircularReference2B => CircularReference2C => CircularReference2

            [PageAt(typeof(CircularReference2A), null)]
            private class CircularReference2 : Page
            {
                public CircularReference2(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(CircularReference2B), null)]
            private class CircularReference2A : Page
            {
                public CircularReference2A(PageSession session = null) : base(session) { }
            }

            [PageAt(typeof(CircularReference2), null)]
            private class CircularReference2B : Page
            {
                public CircularReference2B(PageSession session = null) : base(session) { }
            }
    }
}