using System;

namespace PageObject.Tests.PageConstructor
{
    internal class TestPage : Page
    {
        internal TestPage(string url) : base(null, url) {}

        internal TestPage(Uri uri) : base(null, uri) {}

        internal TestPage(Uri uri, string path) : base(null, uri, path) {}

        internal TestPage(string url, string path) : base(null, url, path) {}
    }
}