using System;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;

namespace PageObject.Tests
{
    [PageAt(FullUrl)]
    internal class HomePage : Page
    {
        internal const string FullUrl = Scheme + "://" + Host + Port + "/" + Path;
        internal const string Scheme = "file";
        internal const string Host = "";
        internal const string Port = "";
        internal const string Path = "${cd}/../../Pages/File/Home.html";

        public HomePage(PageSession session) : base(session) {}
    }

    [TestFixture]
    public class OnPageTests
    {
        private PageSession session;

        [TestFixtureSetUp]
        public void VisitTheHomePage()
        {
            var configuration = new SessionConfiguration { Browser = Browser.PhantomJS };
            session = new PageSession(configuration);
            var page = new HomePage(session);
            page.Visit();
        }

        [TestCase(typeof(HomePage))]
        [TestCase(typeof(SameUrl))]
        [TestCase(typeof(MatchingPort))]
        [TestCase(typeof(MatchingScheme))]
        [TestCase(typeof(MatchingHost))]
        [TestCase(typeof(MatchingPath))]
        public void Should_be_on_pages_that_match(Type pageClass)
        {
            var anotherPage = PageFactory.Instance.PageFor(pageClass, session);

            Assert.That(anotherPage.IsActive, Is.True);
        }

            [PageAt(HomePage.FullUrl)]
            private class SameUrl : Page
            {
                public SameUrl(PageSession session) : base(session) { }
            }

            [PageAt("file2://" + HomePage.Host + "/" + HomePage.Path, SchemeMatch = new[]{"file"})]
            private class MatchingScheme : Page
            {
                public MatchingScheme(PageSession session) : base(session) { }
            }

            [PageAt("http://localhost:80/" + HomePage.Path, SchemeMatch = new[] { "file" }, HostMatch = ".*", PortMatch = new[]{-1})]
            private class MatchingPort : Page
            {
                public MatchingPort(PageSession session) : base(session) { }
            }

            [PageAt("file://localhost/${cd}/../../Pages/File/Home.html", HostMatch = ".*")]
            private class MatchingHost : Page
            {
                public MatchingHost(PageSession session) : base(session) { }
            }

            [PageAt("file:///${cd}/../../Pages/File2/Home.html", PathMatch = @"Pages/File\d?/Home.html")]
            private class MatchingPath : Page
            {
                public MatchingPath(PageSession session) : base(session)
                {
                }
            }

        [TestCase(typeof(DifferentUrl))]
        [TestCase(typeof(DifferentScheme))]
        [TestCase(typeof(DifferentHost))]
        [TestCase(typeof(DifferentPath))]
        public void Should_not_be_on_pages_that_dont_match(Type pageClass)
        {
            var anotherPage = PageFactory.Instance.PageFor(pageClass, session);

            Assert.That(anotherPage.IsActive, Is.False);
        }

            [PageAt("file:///a_different_url")]
            private class DifferentUrl : Page
            {
                public DifferentUrl(PageSession session) : base(session) { }
            }

            [PageAt("file2://" + HomePage.Host + HomePage.Port + "/" + HomePage.Path)]
            private class DifferentScheme : Page
            {
                public DifferentScheme(PageSession session) : base(session) { }
            }

            [PageAt("file://" + "different_host" + HomePage.Port + "/" + HomePage.Path)]
            private class DifferentHost : Page
            {
                public DifferentHost(PageSession session) : base(session) { }
            }

            [PageAt("file://" + HomePage.Host + HomePage.Port + "/" + "different_path")]
            private class DifferentPath : Page
            {
                public DifferentPath(PageSession session) : base(session) { }
            }

        [Test]
        [TestCase(typeof(Child))]
        [TestCase(typeof(Grandchild))]
        public void Should_inherit_base_page_match(Type pageClass)
        {
            var anotherPage = PageFactory.Instance.PageFor(pageClass, session);

            Assert.That(anotherPage.IsActive, Is.True);
        }
//        [PageAt("file2://" + HomePage.Host + "/" + HomePage.Path, SchemeMatch = new[] { "file" })]

        [PageAt("file2://localhost/${cd}/../../Pages/File/Home.html", HostMatch = ".*", SchemeMatch = new[] { "file" })]
            private class Parent : Page
            {
                public Parent(PageSession session) : base(session) { }
            }

            [PageAt(typeof(Parent))]
            private class Child : Page
            {
                public Child(PageSession session) : base(session) { }
            }

            [PageAt(typeof(Child))]
            private class Grandchild : Page
            {
                public Grandchild(PageSession session) : base(session) { }
            }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
