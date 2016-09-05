using PageObject;

namespace PageObjectTests.Pages.AttributeTests
{
    [Page(typeof(string))]
    public class WithParentThatIsNotAPage : Page
    {
        public WithParentThatIsNotAPage(PageSession session = null) : base(session) {}
    }
}

