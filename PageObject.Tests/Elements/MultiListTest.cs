using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;
using static OpenQA.Selenium.Keys;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class MultiListTest : InputTest<TestPage<MultiList>, MultiList>
    {
        private MultiList MultiList => Element;

        protected override string ElementHtml =>@"
            <select name='name' multiple=''>
                <option value='one'>first</option>
                <option value='two'>second</option>
                <option value='three'>third</option>
            </select>";

        [Test]
        public void Value_should_be_empty_when_no_options_are_selected()
        {
            MultiList.Value = new List<string>();
            Assert.That(MultiList.Value, Is.Empty);
        }

        [Test]
        public void Setting_value_to_an_empty_list_should_deselect_previously_selected_options()
        {
            MultiList.Select("first");
            MultiList.Select("second");
            MultiList.Select("third");

            MultiList.Value = new List<string>();
            Assert.That(MultiList.Value, Is.Empty);
        }

        [Test]
        public void Setting_value_should_allow_single_option_to_be_set()
        {
            var selected = new List<string> { "first" };
            MultiList.Value = selected;
            Assert.That(MultiList.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_value_should_allow_multiple_options_to_be_set()
        {
            var selected = new List<string> { "first", "second" };
            MultiList.Value = selected;
            Assert.That(MultiList.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_empty_value_should_deselect_all_selections()
        {
            MultiList.Value = new List<string> { "first", "second" };
            MultiList.Value = new List<string>();
            Assert.That(MultiList.Value, Is.Empty);
        }

        [Test]
        public void Select_should_select_a_previously_unselected_option()
        {
            MultiList.Select("first");
            Assert.That(MultiList.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Select_should_support_multiple_options()
        {
            MultiList.Select("first", "second");
            Assert.That(MultiList.Value, Is.EqualTo(new List<string> { "first", "second" }));
        }

        [Test]
        public void Select_should_be_idempotent()
        {
            MultiList.Select("first");
            MultiList.Select("first");
            Assert.That(MultiList.Value, Has.Exactly(1).EqualTo("first"));
        }

        [Test]
        public void Deselect_should_select_a_previously_unselected_option()
        {
            MultiList.Select("first");
            MultiList.Deselect("first");
            Assert.That(MultiList.Value, Is.Empty);
        }

        [Test]
        public void Deselect_should_be_idempotent()
        {
            MultiList.Select("first");
            MultiList.Deselect("first");
            MultiList.Deselect("first");
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Deselect_should_support_multiple_options()
        {
            MultiList.Select("first", "second");
            MultiList.Deselect("second", "third");
            Assert.That(MultiList.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Click_should_select_a_previously_unselected_option()
        {
            MultiList.Click("first");
            Assert.That(MultiList.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Click_should_be_idempotent()
        {
            MultiList.Click("first");
            MultiList.Click("first");
            Assert.That(MultiList.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Options_should_return_all_options()
        {
            Assert.That(MultiList.Options, Is.EqualTo(new List<string> { "first", "second", "third"}));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(MultiList.Text, Is.StringMatching(@"^\s*first\s*second\s*third\s*$"));
        }

        [Test]
        public void Sendkeys_of_Down_should_select_the_next_option()
        {
            MultiList.Select("first");
            MultiList.SendKeys(Down);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "second" }));
        }

        [Test]
        public void Sendkeys_of_Up_should_select_the_previous_option()
        {
            MultiList.Select("second");
            MultiList.SendKeys(Up);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Sendkeys_of_Home_should_select_the_first_option()
        {
            MultiList.Select("third");
            MultiList.SendKeys(Home);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "first" }));
        }

        [Test]
        public void Sendkeys_of_End_should_select_the_last_option()
        {
            MultiList.Select("first");
            MultiList.SendKeys(End);
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "third" }));
        }

        [Test]
        public void Sendkeys_of_single_character_should_select_the_option_starting_with_that_letter()
        {
            MultiList.Select("first");
            MultiList.SendKeys("t");
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "third" }));
        }
    }
}
