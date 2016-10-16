using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [PageAt("file:///${cd}/" + TestConstants.HtmlFileName)]
    public class TestPage<T> : Page where T : HtmlElement
    {
        public TestPage(PageSession session) : base(session) {}

        [Element("name")]
        public T Element => Element<T>();
    }
}
