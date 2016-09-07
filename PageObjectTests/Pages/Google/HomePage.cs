using PageObject;

namespace PageObjectTests.Pages.Google
{
    [PageObject("http://www.google.com")]
    public class HomePage : Page
    {
        public HomePage(PageSession session) : base(session)
        {
        }
    }
}