using PageObject.Elements;

namespace PageObject.Tests.Elements.SelectElement
{
    [PageAt(ElementTest.ElementsFolder + @"SelectElement\TestPage.html")]
    internal class TestPage : Page
    {
        public TestPage(PageSession session) : base(session) {}

        [Element("name")]
        public Select Element => Element<Select>();
    }
}
