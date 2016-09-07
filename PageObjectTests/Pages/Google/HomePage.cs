using PageObject;

namespace PageObjectTests.Pages.Google
{
    [PageAt("http://www.google.com")]
    public class HomePage : Page
    {
        public HomePage(PageSession session) : base(session)
        {
        }
    }
}
