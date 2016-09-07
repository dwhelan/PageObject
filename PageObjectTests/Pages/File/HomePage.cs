using System;
using PageObject;

namespace PageObjectTests.Pages.File
{
    internal class HomePage : Page
    {
        internal new static Uri Uri => new Uri(Root.Uri, "Home.html");
        internal new static string Url => Uri.AbsoluteUri;

        public HomePage(PageSession session) : base(session, Uri)
        {
        }

        // The Following constructors are used to test PageObject construction

        public HomePage(PageSession session, Uri uri) : base(session, uri)
        {
        }

        public HomePage(PageSession session, Uri uri, string path) : base(session, uri, path)
        {
        }

        public HomePage(PageSession session, string url) : base(session, url)
        {
        }

        public HomePage(PageSession session, string url, string path) : base(session, url, path)
        {
        }
    }
}
