using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SelectElementTest : InputTest<TestPage<SelectElement>, SelectElement>
    {
        private SelectElement SelectElement => Element;

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
            SelectElement.Select("first");
            Assert.That(SelectElement.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Setting_value_should_select_option()
        {
            SelectElement.Value = "first";
            Assert.That(SelectElement.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_keep_option_selected_when_selected_multiple_times()
        {
            SelectElement.Value = "first";
            SelectElement.Value = "first";
            Assert.That(SelectElement.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(SelectElement.Text, Is.StringMatching(@"^\s*first\s*second\s*third\s*$"));
        }

        [Test]
        public void Should_be_able_to_select_by_clicking()
        {
            SelectElement.Click("first");
            Assert.That(SelectElement.Value, Is.EqualTo("first"));
        }

        public void Should_keep_option_selected_when_clicked_multiple_times()
        {
            SelectElement.Click("first");
            SelectElement.Click("first");
            Assert.That(SelectElement.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Options_should_return_all_values()
        {
            Assert.That(SelectElement.Options, Is.EqualTo(new List<string> { "first", "second", "third" }));
        }
    }
}
