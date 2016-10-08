using System;
using PageObject.Elements;

namespace PageObject.Tests.Elements.SelectElement
{
    [PageAt(Foo.ElementsFolder + @"SelectElement\TestPage.html")]
    public class TestPage : BasePage<Select>
    {
        public TestPage(PageSession session) : base(session) {}

        [Element("name")]
        public override Select TestElement => Element<Select>();
    }
}
