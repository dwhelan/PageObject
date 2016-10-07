using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements.SelectElement
{
    [TestFixture]
    public class Test : ElementTest
    {
        private Select Element => ((TestPage) Page).Element;

        protected override Page CreatePage(PageSession session)
        {
            return new TestPage(session);
        }

        [Test]
        public void Should_be_able_to_select_option_by_value()
        {
            Element.Selected = "one";
            Assert.That(Element.Value, Is.EqualTo("one"));
        }

        [Test]
        public void Should_be_able_to_select_option_by_text()
        {
            Element.Selected = "Second option";
            Assert.That(Element.Selected, Is.EqualTo("Second option"));
        }
    }
}
