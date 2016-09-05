using PageObject;

namespace PageObjectTests.Pages.Google
{
    [Page(typeof(GooglePage), "services")]
    public class ServicesPage : Page
    {
        public ServicesPage(PageSession session = null) : base(session)
        {
        }
    }

    [Page("http://www.google.com/services")]
    public class ServicesPageWithUrlOnly : Page
    {
        public ServicesPageWithUrlOnly(PageSession session = null) : base(session)
        {
        }
    }
}