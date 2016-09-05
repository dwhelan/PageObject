using PageObject;

namespace PageObjectTests.Pages.AttributeTests
{
    [Page(Constants.Url)]
    public class WithPathOnly : Page
    {
        public WithPathOnly(PageSession session = null) : base(session) {}
    }
}
