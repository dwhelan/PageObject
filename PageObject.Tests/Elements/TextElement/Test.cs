using NUnit.Framework;
using Text = PageObject.Elements.Text;

namespace PageObject.Tests.Elements.TextElement
{
    [TestFixture]
    public class Test : ElementTest
    {
        private Text Text => ((TestPage) Page).Text;

        protected override Page CreatePage(PageSession session)
        {
            return new TestPage(session);
        }

        [Test]
        public void Should_get_value()
        {
            Assert.That(Text.Value, Is.EqualTo("initial password"));
        }

        [Test]
        public void Should_set_value()
        {
            Text.Value = "new password";
            Assert.That(Text.Value, Is.EqualTo("new password"));
        }
    }
}
