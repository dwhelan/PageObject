using System;
using PageObject;

namespace PageObjectTests.Pages.PageConstructor
{
    internal class ConstructorTestPage : Page
    {
        internal new static Uri Uri => new Uri(Root.Uri, "Home.html");
        internal new static string Url => Uri.AbsoluteUri;

        internal ConstructorTestPage() : base(null, Uri)
        {
        }

        // The Following constructors are used to test PageObject construction

        internal ConstructorTestPage(Uri uri) : base(null, uri)
        {
        }

        internal ConstructorTestPage(Uri uri, string path) : base(null, uri, path)
        {
        }

        internal ConstructorTestPage(string url) : base(null, url)
        {
        }

        internal ConstructorTestPage(string url, string path) : base(null, url, path)
        {
        }
    }
}
