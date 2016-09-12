using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    [TestFixture]
    public class WithBasePageClass : BaseTest
    {
        [PageAt(BaseTest.Url)]
        private class BasePage : Page
        {
            public BasePage() : base(null) { }
            public BasePage(PageSession session) : base(session) { }
        }

        private class DependentPage : Page
        {
            internal DependentPage() : base(null, typeof(BasePage)) { }
            internal DependentPage(Type basePage) : base(null, basePage) { }
            internal DependentPage(Type basePage, string path) : base(null, basePage, path) {}
        }

        [Test]
        public void Should_support_a_page_only()
        {
            AssertValidPage(new DependentPage(typeof(BasePage)));
        }

        [Test]
        public void Should_support_a_base_page_and_path()
        {
            AssertValidPage(new DependentPage(typeof(BasePage), Path));
        }

        [Test]
        public void Should_support_a_null_base_page_with_a_full_path_url()
        {
            AssertValidPage(new DependentPage(null, Url));
        }

        [Test]
        public void Should_ensure_a_valid_path_with_a_null_base_page()
        {
            AssertInvokeThrows<PageObjectException, UriFormatException>(() => new DependentPage(null, "invalid url"));
        }

        [TestCase(typeof(SelfReferencingPage), @"Page .*SelfReferencingPage cannot have itself as a base page")]
        [TestCase(typeof(CircularReference1), @"Detected circular base page references with .*CircularReference1A")]
        //[TestCase(typeof(CircularReference2), @"Detected circular base page references with .*CircularReference2 and .*CircularReference2C")]
        public void Should_detect_circular_references_in_base_pages(Type pageClass, string regEx)
        {
            AssertInvokeThrows<PageObjectException>(() => CreatePage(pageClass), regEx);
        }
            private class SelfReferencingPage : Page
            {
                public SelfReferencingPage(PageSession sesssion = null) : base(sesssion, typeof(SelfReferencingPage)) {}
            }

            public class CircularReference1 : Page
            {
                public CircularReference1(PageSession sesssion = null) : base(sesssion, typeof(CircularReference1A)) {}
            }

            private class CircularReference1A : Page
            { 
                public CircularReference1A(PageSession sesssion = null) : base(sesssion, typeof(CircularReference1)) {}
            }
    }
}
