using PageObject.Elements;

namespace PageObject.Tests.Elements.TextElement
{
    [PageAt(Foo.ElementsFolder + @"TextElement\TestPage.html")]
    public class TestPage : BasePage<Text>
    {
        public TestPage(PageSession session) : base(session) {}
    }
}
