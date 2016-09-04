using System;
using System.IO;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject;

namespace PageObjectTests
{
    [TestFixture]
    public class PageConstructorTests
    {
        private PageSession session;

        [TestFixtureSetUp]
        public void CreateSession()
        {
            session = new PageSession(new SessionConfiguration{ Browser = Browser.PhantomJS });
        }

        [Test]
        public void Should_support_uri_only()
        {
            var uri = new Uri(FilePages.RootUri(), "Home.html");
            AssertFileHomPageTitle(new FileHomePage(session, uri));
        }

        [Test]
        public void Should_support_uri_and_relative_path()
        {
            AssertFileHomPageTitle(new FileHomePage(session, FilePages.RootUri(), "Home.html"));
        }

        [Test]
        public void Should_support_with_url_only()
        {
            var url = new Uri(FilePages.RootUri(), "Home.html").AbsolutePath;
            AssertFileHomPageTitle(new FileHomePage(session, url));
        }

        [Test]
        public void Should_support_with_url_and_relative_path()
        {
            AssertFileHomPageTitle(new FileHomePage(session, FilePages.RootUri(), "Home.html"));
        }

        [TestFixtureTearDown]
        public void DisposeBrowser()
        {
            session?.Dispose();
        }

        private static void AssertFileHomPageTitle(Page page)
        {
            page.Visit();
            Assert.That(page.Title, Is.EqualTo("File Home Page"));
        }
    }

    internal static class FilePages
    {
        internal static Uri RootUri()
        {
            return new UriBuilder(Uri.UriSchemeFile, "", 80, RootPath()).Uri;
        }

        internal static string RootUrl()
        {
            return RootUri().AbsoluteUri;
        }

        internal static string RootPath()
        {
            return Directory.GetCurrentDirectory() + @"\..\..\FilePages\";
        }
    }

    public class FileHomePage : Page
    {

        public FileHomePage(PageSession session, Uri uri) : base(session, uri)
        {
        }

        public FileHomePage(PageSession session, Uri uri, string relativePath) : base(session, uri, relativePath)
        {
        }

        public FileHomePage(PageSession session, string url) : base(session, url)
        {
        }

        public FileHomePage(PageSession session, string url, string relativePath) : base(session, url, relativePath)
        {
        }

    }
}
