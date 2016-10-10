using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SelectElementTest : ElementTest<TestPage<SelectElement>, SelectElement>
    {
        protected override string ElementHtml =>
                                    @"<select name='name'>
                                        <option value='one'>First</option>
                                        <option value='two'>Second</option>
                                        <option value='three'>Three</option>
                                      </select>";

        [Test]
        public void Setting_Value_should_select_option()
        {
            Element.Value = "First";
            Assert.That(Element.Value, Is.EqualTo("First"));
        }

        [Test]
        public void Select_should_select_option()
        {
            Element.Select("Second");
            Assert.That(Element.Value, Is.EqualTo("Second"));
        }

        [Test]
        public void Should_keep_option_selected_when_selected_multiple_times()
        {
            Element.Value = "First";
            Element.Value = "First";
            Assert.That(Element.Value, Is.EqualTo("First"));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.StringMatching(@"^\s*First\s*Second\s*Three\s*$"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }

        [TestFixture]
        public class DisabledSelectElementTest : ElementTest<TestPage<SelectElement>, SelectElement>
        {
            protected override string ElementHtml => @"<select name='name' disabled/>";

            [Test]
            public void Should_be_disabled()
            {
                Assert.That(Element.Enabled, Is.False);
                Assert.That(Element.Disabled, Is.True);
            }
        }
    }
}
