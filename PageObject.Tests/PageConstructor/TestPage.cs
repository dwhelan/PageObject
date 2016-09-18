namespace PageObject.Tests.PageConstructor
{
    internal class TestPage : Page
    {
        internal TestPage(string url) : base(null, url) {}

        internal TestPage(string url, string path) : base(null, url, path) {}
    }
}
