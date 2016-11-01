using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PageObject.Elements;
using WatiN.Core;

namespace PageObject.Tests.Elements
{
    [PageAt("file:///${cd}/" + TestConstants.HtmlFileName)]
    public class TestPage<T> : Page where T : BaseElement
    {
        public TestPage(PageSession session) : base(session) {}

        [Element("name")]
        public T Element => Element<T>();

        [Element("list[]")]
        public ElementList<T> ElementList => ElementList<T>();
    }
}
