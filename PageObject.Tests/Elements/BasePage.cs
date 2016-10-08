using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [PageAt("file:///${cd}/ElementTestPage.html")]
    public class BasePage<T> : Page where T : Element
    {
        public BasePage(PageSession session) : base(session) {}

        [Element("name")]
        public T Element => Element<T>();
    }
}
