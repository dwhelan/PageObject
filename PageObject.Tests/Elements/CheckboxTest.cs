using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class CheckboxTest : InputTest<TestPage<Checkbox>, Checkbox>
    {
        protected override string ElementHtml => @"<input type='checkbox' name='name'>text</input>";

        [Test]
        public void Should_be_able_to_select()
        {
            Element.Select();
            Assert.That(Element.Value, Is.True);
        }

        [Test]
        public void Select_should_be_idempotent()
        {
            Element.Select();
            Element.Select();
            Assert.That(Element.Value, Is.True);
        }

        [Test]
        public void Should_be_able_to_deselect()
        {
            Element.Deselect();
            Assert.That(Element.Value, Is.False);
        }

        [Test]
        public void Deselect_should_be_idempotent()
        {
            Element.Deselect();
            Element.Deselect();
            Assert.That(Element.Value, Is.False);
        }

        [Test]
        public void Should_be_able_to_check_by_setting_value_to_true()
        {
            Element.Value = true;
            Assert.That(Element.Value, Is.True);
        }

        [Test]
        public void Should_be_able_to_uncheck_by_setting_value_to_false()
        {
            Element.Value = false;
            Assert.That(Element.Value, Is.False);
        }

        [Test]
        public void Should_provide_text()
        {
            Assert.That(Element.Text, Is.EqualTo("text"));
        }
    }
}
