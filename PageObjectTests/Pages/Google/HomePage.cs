using PageObject;

namespace PageObjectTests.Pages.Google
{
    [Page("http://www.google.com")]
    public class HomePage : Page
    {
        public HomePage(PageSession session) : base(session)
        {
        }
    }
}