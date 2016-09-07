using System;
using System.Collections.Generic;
using System.Reflection;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject;
using PageObjectTests.Pages.File;
using PageObjectTests.Pages.PageObjectAttribute;

namespace PageObjectTests
{
    [TestFixture]
    public class PageConstructorTests
    {
        private const string Url = BaseUrl + Path;
        private static readonly Uri Uri = new Uri(Url);

        private const string BaseUrl = "file:///";
        private static readonly Uri BaseUri = new Uri(BaseUrl);

        private const string Path = "something";

        private class TestPage : Page
        {
            // The Following constructors are used to test PageObject construction

            internal TestPage(string url) : base(null, url)
            {
            }

            internal TestPage(Uri uri) : base(null, uri)
            {
            }

            internal TestPage(Uri uri, string path) : base(null, uri, path)
            {
            }

            internal TestPage(string url, string path) : base(null, url, path)
            {
            }
        }

        [Test]
        public void Should_support_url_only()
        {
            AssertValidPage(new TestPage(Url));
        }

        [Test]
        public void Should_ensure_a_valid_url()
        {
            AssertThrowsPageObjectException(() => new TestPage("invalid url"));
        }

        [TestCase(BaseUrl, Path)]
        [TestCase(Url, "")]
        [TestCase(Url, null)]
        public void Should_support_a_base_url(string baseUrl, string path)
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

        [Test]
        public void Should_support_uri_only()
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
            AssertValidPage(new TestPage((Uri) null, Url));
        }

        private static void AssertThrowsPageObjectException(Func<TestPage> func)
        {
            try
            {
                func.DynamicInvoke();
            }
            catch (TargetInvocationException x)
            {
                Assert.That(x.InnerException, Is.AssignableTo(typeof(PageObjectException)));
                Assert.That(x.InnerException.InnerException, Is.AssignableTo(typeof(UriFormatException)));
            }
        }

        private static void AssertValidPage(Page page)
        {
            Assert.That(page.Url, Is.EqualTo(Url));
        }
    }
}
