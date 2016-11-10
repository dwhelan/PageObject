using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [PageAt(TestConstants.TestPageUrl)]
    public class TestPage<T> : Page where T : BaseElement
    {
        public TestPage(PageSession session) : base(session) {}

        [Element("name")]
        public T Element => Element<T>();

        [Element("list[]")]
        public ElementList<T> ElementList => ElementList<T>();
    }
}
