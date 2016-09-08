using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    [TestFixture]
    public class WithBaseUrl : BaseTest
    {
        [Test]
        public void Should_support_just_a_path()
        {
            AssertValidPage(new TestPage(Url));
        }

        [Test]
        public void Should_ensure_a_valid_path()
        {
            AssertThrowsPageObjectException(() => new TestPage("invalid url"));
        }

        [TestCase(BaseUrl, Path)]
        [TestCase(Url, "")]
        [TestCase(Url, null)]
        public void Should_support_a_base_url_and_path(string baseUrl, string path)
        {
            AssertValidPage(new TestPage(baseUrl, path));
        }

        [Test]
        public void Should_support_a_null_base_url_with_a_full_path_url()
        {
            AssertValidPage(new TestPage((string)null, Url));
        }

        [Test]
        public void Should_ensure_a_valid_base_url()
        {
            AssertThrowsPageObjectException(() => new TestPage("invalid url", Path));
        }
    }
}