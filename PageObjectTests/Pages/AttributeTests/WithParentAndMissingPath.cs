using PageObject;

namespace PageObjectTests.Pages.AttributeTests
{
    [Page(typeof(WithPathOnly))]
    public class WithParentAndMissingPath : Page
    {
        public WithParentAndMissingPath(PageSession session = null) : base(session) {}
    }
}
