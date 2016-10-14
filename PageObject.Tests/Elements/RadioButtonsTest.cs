﻿using System.Collections.Generic;
using Coypu;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class RadioButtonsTest : ElementTest<TestPage<RadioButtons>, RadioButtons>
    {
        protected override string ElementHtml => $@"
            {RadioButton1}<br/> 
            {RadioButton2}<br/> 
            {RadioButton3}<br/> 
            {RadioButton4}<br/> 
            {RadioButton5}<br/> 
            {OtherRadioButton}<br/>
        ";

        private const string OtherRadioButton = @"
            <label>
                <input type='radio' name='other' id='id1' value='value1'/>other label1
            </label>
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

        [Test]
        public void Value_should_be_an_empty_string_when_no_radio_buttons_chosen()
        {
            Assert.That(Element.Value, Is.EqualTo(""));
        }

        [Test]
        public void Should_set_by_value_attribute()
        {
            Element.Value = "value1";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_set_by_id_attribute()
        {
            Element.Value = "id1";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_choose_by_parent_label()
        {
            Element.Value = "first";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_choose_by_label()
        {
            Element.Value = "second";
            Assert.That(Element.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Should_choose_by_any_label()
        {
            Element.Value = "otherSecond";
            Assert.That(Element.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Should_choose_by_all_labels_together()
        {
            Element.Value = "second otherSecond";
            Assert.That(Element.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Should_choose_by_all_labels_together_removing_spaces()
        {
            Element.Value = " \n\tsecond   otherSecond  ";
            Assert.That(Element.Value, Is.EqualTo("second otherSecond"));
        }

        [Test]
        public void Value_should_combine_labels_for_and_parent_label()
        {
            Element.Value = "third";
            Assert.That(Element.Value, Is.EqualTo("third otherThird yetAnotherThird"));
        }

        [Test]
        public void Value_should_return_value_attribute_if_no_labels_exist()
        {
            Element.Value = "id4";
            Assert.That(Element.Value, Is.EqualTo("value4"));
        }

        [Test]
        public void Value_should_return_on_if_selected_and_no_value_attribute_if_no_labels_exist()
        {
            Element.Value = "id5";
            Assert.That(Element.Value, Is.EqualTo("on"));
        }

        [Test]
        public void Options_should_return_all_values()
        {
            Assert.That(Element.Options, Is.EqualTo(new List<string> { "first", "second otherSecond", "third otherThird yetAnotherThird", "value4", "on"}));
        }

        [TestFixture]
        public class NoRadioButtonsTest : ElementTest<TestPage<RadioButtons>, RadioButtons>
        {
            protected override string ElementHtml => "";

            [Test]
            public void Getting_value_should_throw()
            {
                Assert.Throws<MissingHtmlException>(() => { var x = Element.Value; });
            }

            [Test]
            public void Setting_bad_value_should_throw()
            {
                Assert.Throws<MissingHtmlException>(() => { Element.Value = "bad value"; });
            }
        }
    }
}
