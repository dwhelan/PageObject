using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class CheckboxElementTest : ElementTest<TestPage<CheckboxElement>, CheckboxElement>
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
        public void Should_be_able_to_check_by_setting_value_to_true()
        {
            Element.Value = true;
            Assert.That(Element.Checked, Is.True);
        }

        [Test]
        public void Should_be_able_to_uncheck_by_setting_value_to_false()
        {
            Element.Value = false;
            Assert.That(Element.Checked, Is.False);
        }

        [Test]
        public void Should_provide_text()
        {
            Assert.That(Element.Text, Is.EqualTo("text"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }
    }


    [TestFixture]
    public class DisabledCheckboxElementTest : ElementTest<TestPage<CheckboxElement>, CheckboxElement>
    {
        protected override string ElementHtml => @"<input type='checkbox' name='name' disabled>text</input>";

        [Test]
        public void Should_be_disabled()
        {
            Assert.That(Element.Enabled, Is.False);
            Assert.That(Element.Disabled, Is.True);
        }
    }

}
