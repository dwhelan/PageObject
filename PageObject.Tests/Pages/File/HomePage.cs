namespace PageObject.Tests.Pages.File
{
    [PageAt("file:///Z:/code/cs/PageObject/PageObject.Tests/Pages/File/Home.html")]
    internal class HomePage : Page
    {
        public HomePage(PageSession session) : base(session)
        {
        }
    }

    [PageAt("file:///Z:/code/cs/PageObject/PageObject.Tests/Pages/File/Home.html")]
    internal class HomePage2 : Page
    {
        public HomePage2(PageSession session) : base(session)
        {
        }
    }

    [PageAt("file://localhost/Z:/code/cs/PageObject/PageObject.Tests/Pages/File/Home.html", HostMatch=".*")]
    internal class HomePage3 : Page
    {
        public HomePage3(PageSession session) : base(session)
        {
        }
    }
}