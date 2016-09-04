using System;
using PageObject;

namespace PageObjectTests.FilePages
{
    internal class HomePage : Page
    {
        internal new static Uri Uri => new Uri(Root.Uri, "Home.html");
        internal new static string Url => Uri.AbsolutePath;

        public HomePage(PageSession session) : base(session, Uri)
        {
        }

        // The Following constructors are used to test PageObject construction

        public HomePage(PageSession session, Uri uri) : base(session, uri)
        {
        }

        public HomePage(PageSession session, Uri uri, string relativePath) : base(session, uri, relativePath)
        {
        }

        public HomePage(PageSession session, string url) : base(session, url)
        {
        }

        public HomePage(PageSession session, string url, string relativePath) : base(session, url, relativePath)
        {
        }
    }
}