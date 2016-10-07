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
        public void Should_get_value()
        {
            Assert.That(Select.Value, Is.EqualTo("one"));
        }

        [Test]
        public void Should_get_selected()
        {
            Assert.That(Select.Selected, Is.EqualTo("First option"));
        }

        [Test]
        [Ignore]
        public void Should_set_value()
        {
            Select.Value = "new password";
            Assert.That(Select.Value, Is.EqualTo("new password"));
        }
    }
}
