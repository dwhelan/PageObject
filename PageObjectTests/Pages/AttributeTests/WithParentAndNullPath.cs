using PageObject;

namespace PageObjectTests.Pages.AttributeTests
{
    [Page(typeof(WithPathOnly), null)]
    public class WithParentAndNullPath : Page
    {
        public WithParentAndNullPath(PageSession session = null) : base(session) {}
    }
}
