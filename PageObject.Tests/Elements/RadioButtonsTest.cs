using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;
using static OpenQA.Selenium.Keys;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class RadioButtonsTest : BaseElementTest<TestPage<RadioButtons>, RadioButtons>
    {
        private RadioButtons RadioButtons => Element;

        protected override string ElementHtml => $@"
            <div>
                {RadioButtonForInputTests}Must be first!!!<br/>
                {RadioButton1}<br/> 
                {RadioButton2}<br/> 
                {RadioButton3}<br/> 
                {RadioButton4}<br/> 
                {RadioButton5}<br/> 
                {OtherRadioButton}<br/>
            </div>
        ";

        private const string RadioButton1 = @"
            <label>
              <input type='radio' name='name' id='id1' value='value1'/>first
            </label>
        ";

        private const string RadioButton2 = @"
            <input type='radio' name='name' id='id2' value='value2'/>
            <label for='id2'>second</label>
            <label for='id2'>otherSecond</label>
        ";

        private const string RadioButton3 = @"
            <label for='id3'>third</label>
            <label><input type='radio' name='name' id='id3' value='value3'/>otherThird</label>
            <label for='id3'>yetAnotherThird</label>
        ";

        private const string RadioButton4 = @"
             <input type ='radio' name='name' id='id4' value='value4'/>
        ";

        private const string RadioButton5 = @"
             <input type='radio' name='name' id='id5'/>
        ";


        private const string OtherRadioButton = @"
            <label>
                <input type='radio' name='other' id='id1' value='value1'/>other label1
            </label>
        ";


        private const string RadioButtonForInputTests = @"
            <label>
                <input type='radio' name='other' id='name' value='value1'/>other label1
            </label>
        ";

        [Test]
        public void Value_should_be_an_empty_string_when_no_radio_buttons_chosen()
        {
            Assert.That(RadioButtons.Value, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Select_should_select()
        {
            RadioButtons.Select("value1");
            Assert.That(RadioButtons.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Setting_value_should_select_by_value_attribute()
        {
            RadioButtons.Value = "value1";
            Assert.That(RadioButtons.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Setting_value_should_select_by_id_attribute()
        {
            RadioButtons.Value = "id1";
            Assert.That(RadioButtons.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Setting_value_should_select_by_parent_label()
        {
            RadioButtons.Value = "first";
            Assert.That(RadioButtons.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Setting_value_should_select_by_label()
        {
            RadioButtons.Value = "second";
            Assert.That(RadioButtons.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Setting_value_should_select_by_any_label()
        {
            RadioButtons.Value = "otherSecond";
            Assert.That(RadioButtons.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Setting_value_should_select_by_all_labels_together()
        {
            RadioButtons.Value = "second otherSecond";
            Assert.That(RadioButtons.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Setting_value_should_select_by_all_labels_together_removing_spaces()
        {
            RadioButtons.Value = " \n\tsecond   otherSecond  ";
            Assert.That(RadioButtons.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Value_should_combine_labels_for_and_parent_label()
        {
            RadioButtons.Value = "third";
            Assert.That(RadioButtons.Value, Is.EqualTo("third otherThird yetAnotherThird"));
        }

        [Test]
        public void Value_should_return_value_attribute_if_no_labels_exist()
        {
            RadioButtons.Value = "id4";
            Assert.That(RadioButtons.Value, Is.EqualTo("value4"));
        }

        [Test]
        public void Value_should_return_on_if_selected_and_no_value_attribute_if_no_labels_exist()
        {
            RadioButtons.Value = "id5";
            Assert.That(RadioButtons.Value, Is.EqualTo("on"));
        }

        [Test]
        public void Options_should_return_all_values()
        {
            Assert.That(RadioButtons.Options, Is.EqualTo(new List<string> { "first", "second otherSecond", "third otherThird yetAnotherThird", "value4", "on"}));
        }

        [Test]
        public void Click_should_select()
        {
            RadioButtons.Click("first");
            Assert.That(RadioButtons.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Sendkeys_of_Down_should_select_the_next_option()
        {
            RadioButtons.Select("first");
            RadioButtons.SendKeys(Down);
            Assert.That(RadioButtons.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Sendkeys_of_Up_should_select_the_next_option()
        {
            RadioButtons.Select("second");
            RadioButtons.SendKeys(Up);
            Assert.That(RadioButtons.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Sendkeys_of_Right_should_select_the_next_option()
        {
            RadioButtons.Select("first");
            RadioButtons.SendKeys(Right);
            Assert.That(RadioButtons.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Sendkeys_of_Left_should_select_the_previous_option()
        {
            RadioButtons.Select("second");
            RadioButtons.SendKeys(Left);
            Assert.That(RadioButtons.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Sendkeys_should_do_nothing_if_no_option_selected()
        {
            RadioButtons.SendKeys(Down);
            Assert.That(RadioButtons.Value, Is.EqualTo(""));
        }
    }
}
