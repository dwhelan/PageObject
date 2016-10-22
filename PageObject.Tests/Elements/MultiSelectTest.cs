using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;
using static OpenQA.Selenium.Keys;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class MultiSelectTest : InputTest<TestPage<MultiSelect>, MultiSelect>
    {
        private MultiSelect MultiSelect => Element;

        protected override string ElementHtml =>@"
            <select name='name' multiple=''>
                <option value='one'>first</option>
                <option value='two'>second</option>
                <option value='three'>third</option>
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
            MultiSelect.Select("first");
            MultiSelect.Select("second");
            MultiSelect.Select("third");

            MultiSelect.Value = new List<string>();
            Assert.That(MultiSelect.Value, Is.Empty);
        }

        [Test]
        public void Setting_value_should_allow_single_option_to_be_set()
        {
            var selected = new List<string> { "first" };
            MultiSelect.Value = selected;
            Assert.That(MultiSelect.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_value_should_allow_multiple_options_to_be_set()
        {
            var selected = new List<string> { "first", "second" };
            MultiSelect.Value = selected;
            Assert.That(MultiSelect.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_empty_value_should_deselect_all_selections()
        {
            MultiSelect.Value = new List<string> { "first", "second" };
            MultiSelect.Value = new List<string>();
            Assert.That(MultiSelect.Value, Is.Empty);
        }

        [Test]
        public void Select_should_select_a_previously_unselected_option()
        {
            MultiSelect.Select("first");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Select_should_support_multiple_options()
        {
            MultiSelect.Select("first", "second");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "first", "second" }));
        }

        [Test]
        public void Select_should_be_idempotent()
        {
            MultiSelect.Select("first");
            MultiSelect.Select("first");
            Assert.That(MultiSelect.Value, Has.Exactly(1).EqualTo("first"));
        }

        [Test]
        public void Deselect_should_select_a_previously_unselected_option()
        {
            MultiSelect.Select("first");
            MultiSelect.Deselect("first");
            Assert.That(MultiSelect.Value, Is.Empty);
        }

        [Test]
        public void Deselect_should_be_idempotent()
        {
            MultiSelect.Select("first");
            MultiSelect.Deselect("first");
            MultiSelect.Deselect("first");
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Deselect_should_support_multiple_options()
        {
            MultiSelect.Select("first", "second");
            MultiSelect.Deselect("second", "third");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Click_should_select_a_previously_unselected_option()
        {
            MultiSelect.Click("first");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Click_should_be_idempotent()
        {
            MultiSelect.Click("first");
            MultiSelect.Click("first");
            Assert.That(MultiSelect.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Options_should_return_all_options()
        {
            Assert.That(MultiSelect.Options, Is.EqualTo(new List<string> { "first", "second", "third"}));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(MultiSelect.Text, Is.StringMatching(@"^\s*first\s*second\s*third\s*$"));
        }

        [Test]
        public void Sendkeys_of_Down_should_select_the_next_option()
        {
            MultiSelect.Select("first");
            MultiSelect.SendKeys(Down);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "second" }));
        }

        [Test]
        public void Sendkeys_of_Up_should_select_the_previous_option()
        {
            MultiSelect.Select("second");
            MultiSelect.SendKeys(Up);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Sendkeys_of_Home_should_select_the_first_option()
        {
            MultiSelect.Select("third");
            MultiSelect.SendKeys(Home);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Sendkeys_of_End_should_select_the_last_option()
        {
            MultiSelect.Select("first");
            MultiSelect.SendKeys(End);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "third" }));
        }

        [Test]
        public void Sendkeys_of_single_character_should_select_the_option_starting_with_that_letter()
        {
            MultiSelect.Select("first");
            MultiSelect.SendKeys("t");
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "third" }));
        }
    }
}
