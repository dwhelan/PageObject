using System.Collections.Generic;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class MultiSelectElementTest : ElementTest<TestPage<MultiSelectElement>, MultiSelectElement>
    {
        protected override string ElementHtml =>
                                    @"<select name='name' multiple>
                                        <option value='one'>First</option>
                                        <option value='two'>Second</option>
                                        <option value='three'>Third</option>
                                      </select>";

        [Test]
        public void Value_should_be_empty_when_no_options_are_selected()
        {
            var selected = new List<string>();
            Element.Value = selected;
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Setting_value_to_an_empty_list_should_deselect_previously_selected_options()
        {
            Element.Select("First");
            Element.Select("Second");
            Element.Select("Third");

            Element.Value = new List<string>();
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Value_should_be_single_option_when_one_option_is_selected()
        {
            var selected = new List<string> { "First" };
            Element.Value = selected;
            Assert.That(Element.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Value_should_be_multiple_options_when_multiple_options_are_selected()
        {
            var selected = new List<string> { "First", "Second" };
            Element.Value = selected;
            Assert.That(Element.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Select_should_select_a_previously_unselected_option()
        {
            Element.Select("First");
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "First" }));
        }

        [Test]
        public void Select_should_deselect_a_previously_selected_option()
        {
            Element.Select("First");
            Element.Select("First");
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.StringMatching(@"^\s*First\s*Second\s*Third\s*$"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }

        [TestFixture]
        public class DisabledMultiSelectElementTest : ElementTest<TestPage<SelectElement>, SelectElement>
        {
            protected override string ElementHtml => @"<select name='name' multiple disabled/>";

            [Test]
            public void Should_be_disabled()
            {
                Assert.That(Element.Enabled, Is.False);
                Assert.That(Element.Disabled, Is.True);
            }
        }
    }
}
