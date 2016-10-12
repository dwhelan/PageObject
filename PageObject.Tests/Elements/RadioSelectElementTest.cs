using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class RadioSelectElementTest : ElementTest<TestPage<RadioSelectElement>, RadioSelectElement>
    {
        protected override string ElementHtml => @"
            <label><input type='radio' name='other' id='id1' value='first'/>lother label1</label>
            <label><input type='radio' name='name' id='id1' value='first'/>label1</label>
            <input type='radio' name='name' id='id2' value='second'/>
            <label for='id2'>label2</label>
        ";

        [Test]
        public void Value_should_be_an_empty_string_when_no_radio_buttons_chosen()
        {
            Assert.That(Element.Value, Is.EqualTo(""));
        }

        [Test]
        public void Should_set_by_value_attribute()
        {
            Element.Value = "first";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_set_by_id_attribute()
        {
            Element.Value = "id1";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_choose_by_enclosing_label()
        {
            Element.Value = "label1";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_choose_by_related_label()
        {
            Element.Value = "label2";
            Assert.That(Element.Value, Is.EqualTo("second"));
        }

        [Test]
        public void Should_be_able_to_choose_second_checkbox()
        {
            Element.Choose("second");
            Assert.That(Element.Value, Is.EqualTo("second"));
        }
    }
}
