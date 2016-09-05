using PageObject;

namespace PageObjectTests.Pages.AttributeTests
{
    [Page(typeof(WithPathOnly), "")]
    public class WithParentAndEmptyPath : Page
    {
        public WithParentAndEmptyPath(PageSession session = null) : base(session) {}
    }
}
