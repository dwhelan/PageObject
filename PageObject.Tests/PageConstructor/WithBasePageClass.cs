using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    [TestFixture]
    public class WithBasePageClass : BaseTest
    {
        internal class DependentPage : Page
        {
            internal DependentPage(Type basePage) : base(null, basePage)
            {
            }

            internal DependentPage(Page basePage, string path) : base(null, basePage, path)
            {
            }
        }

        [Test, Ignore]
        public void Should_support_a_page_only()
        {
            AssertValidPage(new DependentPage(typeof(TestPage)));
        }
        /*
                [TestCase(BaseUrl, Path)]
                [TestCase(Url, "")]
                [TestCase(Url, null)]
                public void Should_support_a_base_page_and_path(string baseUrl, string path)
                {
                    var basePage = new TestPage(baseUrl);
                    AssertValidPage(new DependentPage(basePage, path));
                }

                [Test]
                public void Should_support_a_null_base_path_with_a_full_path_url()
                {
                    AssertValidPage(new DependentPage(null, Url));
                }

                [Test]
                public void Should_ensure_a_valid_path_with_a_null_base_page()
                {
                    AssertThrowsPageObjectException(() => new DependentPage(null, "invalid url"));
                }

                //[TestCase(typeof(SelfReferencingPage), @"Page .*SelfReferencingPage cannot have itself as a base page")]
                ////[TestCase(typeof(CircularReference1), @"Detected circular base page references with .*CircularReference1 and .*CircularReference1B")]
                ////[TestCase(typeof(CircularReference2), @"Detected circular base page references with .*CircularReference2 and .*CircularReference2C")]
                //public void Should_detect_circular_references_in_base_pages(Type pageClass, string regEx)
                //{
                //    AssertThrowsPageObjectException(() => new SelfReferencingPage());
                //}
                //    [PageAt(typeof(SelfReferencingPage), null)]
                //    private class SelfReferencingPage : Page
                //    {
                //        public SelfReferencingPage() : base(null, typeof(SelfReferencingPage))
                //        {
                //        }
                //    }
            */
    }
}
