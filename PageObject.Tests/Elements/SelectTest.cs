using System.Collections.Generic;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SelectTest : ElementTest<TestPage<Select>, Select>
    {
        protected override string ElementHtml =>
            @"<select name='name'>
                <option value='one'>first</option>
                <option value='two'>second</option>
                <option value='three'>third</option>
                </select>";

        [Test]
        public void Setting_value_should_select_option()
        {
            Element.Value = "first";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_keep_option_selected_when_selected_multiple_times()
        {
            Element.Value = "first";
            Element.Value = "first";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.StringMatching(@"^\s*first\s*second\s*third\s*$"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }

        [Test]
        [Ignore]
        public void Options_should_return_all_values()
        {
            Assert.That(Element.Options, Is.EqualTo(new List<string> { "first", "second", "third" }));
        }

        [TestFixture]
        public class DisabledSelectElementTest : ElementTest<TestPage<Select>, Select>
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
