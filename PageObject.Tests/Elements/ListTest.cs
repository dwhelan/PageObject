using System.Collections.Generic;
using NUnit.Framework;
using static OpenQA.Selenium.Keys;
using List = PageObject.Elements.List;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class ListTest : FieldTest<TestPage<List>, List>
    {
        private List List => Element;

        protected override string ElementHtml => @"
            <select name='name'>
                <option value='one'>first</option>
                <option value='two'>second</option>
                <option value='three'>third</option>
            </select>
        ";

        [Test]
        public void Select_should_select_option()
        {
            List.Select("first");
            Assert.That(List.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Setting_value_should_select_option()
        {
            List.Value = "first";
            Assert.That(List.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_keep_option_selected_when_selected_multiple_times()
        {
            List.Value = "first";
            List.Value = "first";
            Assert.That(List.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(List.Text, Is.StringMatching(@"^\s*first\s*second\s*third\s*$"));
        }

        [Test]
        public void Should_be_able_to_select_by_clicking()
        {
            List.Click("first");
            Assert.That(List.Value, Is.EqualTo("first"));
        }

        public void Should_keep_option_selected_when_clicked_multiple_times()
        {
            List.Click("first");
            List.Click("first");
            Assert.That(List.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Options_should_return_all_values()
        {
            Assert.That(List.Options, Is.EqualTo(new List<string> { "first", "second", "third" }));
        }

        [Test]
        public void Sendkeys_of_Down_should_select_the_next_option()
        {
            List.Select("first");
            List.SendKeys(Down);
            Assert.That(Element.Value, Is.EqualTo("second"));
        }

        [Test]
        public void Sendkeys_of_Up_should_select_the_previous_option()
        {
            List.Select("second");
            List.SendKeys(Up);
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Sendkeys_of_Home_should_select_the_first_option()
        {
            List.Select("third");
            List.SendKeys(Home);
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Sendkeys_of_End_should_select_the_last_option()
        {
            List.Select("first");
            List.SendKeys(End);
            Assert.That(Element.Value, Is.EqualTo("third"));
        }

        [Test]
        public void Sendkeys_of_single_character_should_select_the_option_starting_with_that_letter()
        {
            List.Select("first");
            List.SendKeys("t");
            Assert.That(Element.Value, Is.EqualTo("third"));
        }
    }
}
