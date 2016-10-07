using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements.SelectElement
{
    [TestFixture]
    public class Test : ElementTest
    {
        private Select Select => ((TestPage) Page).Select;

        protected override Page CreatePage(PageSession session)
        {
            return new TestPage(session);
        }

        [Test]
        public void Should_be_able_to_select_option_by_value()
        {
            Select.Selected = "one";
            Assert.That(Select.Value, Is.EqualTo("one"));
        }

        [Test]
        public void Should_be_able_to_select_option_by_text()
        {
            Select.Selected = "Second option";
            Assert.That(Select.Selected, Is.EqualTo("Second option"));
        }
    }
}
