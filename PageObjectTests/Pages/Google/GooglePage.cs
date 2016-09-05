using PageObject;

namespace PageObjectTests.Pages.Google
{
    [Page("http://www.google.com")]
    public class GooglePage : Page
    {
        public GooglePage(PageSession session) : base(session)
        {
        }
    }
}