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
        internal const string Url = BaseUrl + Path;
        internal static readonly Uri Uri = new Uri(Url);

        internal const string BaseUrl = "file:///";
        internal static readonly Uri BaseUri = new Uri(BaseUrl);

        internal const string Path = "something";

        internal class TestPage : Page
        {
            internal new static Uri Uri => new Uri(Pages.PageConstructor.Root.Uri, "Home.html");
            internal new static string Url => Uri.AbsoluteUri;

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

        [Test]
        public void Should_ensure_a_valid_uri()
        {
            AssertThrowsPageObjectException(() => new TestPage("invalid url", Path));
        }

        [Test]
        public void Should_ensure_a_valid_path()
        {
            AssertThrowsPageObjectException(() => new TestPage("invalid url"));
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
