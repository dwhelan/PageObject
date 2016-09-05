using PageObject;

namespace PageObjectTests.Pages.AttributeTests
{
    [Page(typeof(WithPathOnly), "something")]
    public class WithParentAndPath : Page
    {
        public WithParentAndPath(PageSession session = null) : base(session) {}
    }
}
