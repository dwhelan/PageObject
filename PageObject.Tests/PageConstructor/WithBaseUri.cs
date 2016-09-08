using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    [TestFixture]
    public class WithBaseUri : BaseTest
    {
        [Test]
        public void Should_support_just_a_uri()
        {
            AssertValidPage(new TestPage(new Uri(Url)));
        }

        [TestCase(BaseUrl, Path)]
        [TestCase(Url, "")]
        [TestCase(Url, null)]
        public void Should_support_a_base_uri(string baseUrl, string path)
        {
            AssertValidPage(new TestPage(new Uri(baseUrl), path));
        }

        [Test]
        public void Should_support_a_null_base_uri_with_a_full_path_url()
        {
            AssertValidPage(new TestPage((Uri)null, Url));
        }
    }
}
