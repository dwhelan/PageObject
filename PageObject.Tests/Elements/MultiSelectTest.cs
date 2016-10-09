using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class MultiSelectTest : ElementTest<TestPage<MultiSelect>, MultiSelect>
    {
        protected override string ElementHtml =>
                                    @"<select name='name' multiple>
                                        <option value='one'>First option</option>
                                        <option value='two'>Second option</option>
                                      </select>";

        [Test]
        public void Value_should_be_empty_when_no_options_are_selected()
        {
            var selected = new List<string>();
            Element.Value = selected;
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Value_should_be_single_option_when_one_options_is_selected()
        {
            var selected = new List<string> { "First option" };
            Element.Value = selected;
            Assert.That(Element.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Value_should_be_multiple_options_when_multiple_options_are_selected()
        {
            var selected = new List<string> { "First option", "Second option" };
            Element.Value = selected;
            Assert.That(Element.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.StringMatching(@"^\s*First option\s*Second option\s*$"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }
    }

    [TestFixture]
    public class DisabledMultiSelectTest : ElementTest<TestPage<Select>, Select>
    {
        protected override string ElementHtml =>
                                    @"<select name='name' disabled multiple>
                                        <option value='one'>First option</option>
                                        <option value='two'>Second option</option>
                                      </select>";
        [Test]
        public void Should_be_disabled()
        {
            Assert.That(Element.Enabled, Is.False);
            Assert.That(Element.Disabled, Is.True);
        }
    }
}
