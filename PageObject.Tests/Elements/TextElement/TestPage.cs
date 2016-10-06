using PageObject.Elements;

namespace PageObject.Tests.Elements.TextElement
{
    [PageAt(ElementTest.ElementsFolder + @"TextElement\TestPage.html")]
    internal class TestPage : Page
    {
        public TestPage(PageSession session) : base(session) {}

        [Element("name")]
        public Text Text => Element<Text>();
    }
}
