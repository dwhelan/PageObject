using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class MultiSelectTest : InputTest<TestPage<MultiSelect>, MultiSelect>
    {
        private MultiSelect MultiSelect => Element;

        protected override string ElementHtml =>@"
            <select name='name' multiple=''>
                <option value='one'>First</option>
                <option value='two'>Second</option>
                <option value='three'>Third</option>
            </select>";

        [Test]
        public void Value_should_be_empty_when_no_options_are_selected()
        {
            MultiSelect.Value = new List<string>();
            Assert.That(MultiSelect.Value, Is.Empty);
        }

        [Test]
        public void Setting_value_to_an_empty_list_should_deselect_previously_selected_options()
        {
            MultiSelect.Select("First");
            MultiSelect.Select("Second");
            MultiSelect.Select("Third");

            MultiSelect.Value = new List<string>();
            Assert.That(MultiSelect.Value, Is.Empty);
        }

        [Test]
        public void Setting_value_should_allow_single_option_to_be_set()
        {
            var selected = new List<string> { "First" };
            MultiSelect.Value = selected;
            Assert.That(MultiSelect.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_value_should_allow_multiple_options_to_be_set()
        {
            var selected = new List<string> { "First", "Second" };
            MultiSelect.Value = selected;
            Assert.That(MultiSelect.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_empty_value_should_deselect_all_selections()
        {
            MultiSelect.Value = new List<string> { "First", "Second" };
            MultiSelect.Value = new List<string>();
            Assert.That(MultiSelect.Value, Is.Empty);
        }

        [Test]
        public void Select_should_select_a_previously_unselected_option()
        {
            MultiSelect.Select("First");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "First" }));
        }

        [Test]
        public void Select_should_support_multiple_options()
        {
            MultiSelect.Select("First", "Second");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "First", "Second" }));
        }

        [Test]
        public void Select_should_be_idempotent()
        {
            MultiSelect.Select("First");
            MultiSelect.Select("First");
            Assert.That(MultiSelect.Value, Has.Exactly(1).EqualTo("First"));
        }

        [Test]
        public void Deselect_should_select_a_previously_unselected_option()
        {
            MultiSelect.Select("First");
            MultiSelect.Deselect("First");
            Assert.That(MultiSelect.Value, Is.Empty);
        }

        [Test]
        public void Deselect_should_be_idempotent()
        {
            MultiSelect.Select("First");
            MultiSelect.Deselect("First");
            MultiSelect.Deselect("First");
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Deselect_should_support_multiple_options()
        {
            MultiSelect.Select("First", "Second");
            MultiSelect.Deselect("Second", "Third");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "First" }));
        }

        [Test]
        public void Click_should_select_a_previously_unselected_option()
        {
            MultiSelect.Click("First");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "First" }));
        }

        [Test]
        public void Click_should_be_idempotent()
        {
            MultiSelect.Click("First");
            MultiSelect.Click("First");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "First" }));
        }

        [Test]
        public void Options_should_return_all_options()
        {
            Assert.That(MultiSelect.Options, Is.EqualTo(new List<string> { "First", "Second", "Third"}));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(MultiSelect.Text, Is.StringMatching(@"^\s*First\s*Second\s*Third\s*$"));
        }
    }
}
