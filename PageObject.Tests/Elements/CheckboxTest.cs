using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class CheckboxTest : Test<TestPage<Checkbox>, Checkbox>
    {
        protected override string ElementHtml => @"<input type='checkbox' name='name'>text</input>";

        [Test]
        public void Should_be_able_to_check()
        {
            Element.Check();
            Assert.That(Element.Checked, Is.True);
        }

        [Test]
        public void Should_be_able_to_uncheck()
        {
            Element.Uncheck();
            Assert.That(Element.Checked, Is.False);
        }

        [Test]
        public void Should_provide_text()
        {
            Assert.That(Element.Text, Is.EqualTo("text"));
        }
    }
}
