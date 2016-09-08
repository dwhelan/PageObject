using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    [TestFixture]
    public class WithBasePage : BaseTest
    {
        internal class DependentPage : Page
        {
            internal DependentPage(Page basePage) : base(null, basePage) {}

            internal DependentPage(Page basePage, string path) : base(null, basePage, path) {}
        }

        [Test]
        public void Should_support_a_page_only()
        {
            AssertValidPage(new DependentPage(new TestPage(Url)));
        }

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
    }
}
