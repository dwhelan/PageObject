﻿using System.Collections.Generic;
using Coypu;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class RadioTest : ElementTest<TestPage<Radio>, Radio>
    {
        protected override string ElementHtml => @"
            <label><input type='radio' name='other' id='id1' value='value1'/>other label1</label>

            <label><input type='radio' name='name' id='id1' value='value1'/>first</label>
            
            <input type='radio' name='name' id='id2' value='value2'/>
            <label for='id2'>label2</label>
            <label for='id2'>label2a</label>
            
            <label for='id3'>label3</label>
            <label><input type='radio' name='name' id='id3' value='value3'/>third</label>
            <label for='id3'>label3a</label>

            <input type='radio' name='name' id='id4' value='value4'/>

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
        public void Should_choose_by_ancestor_label()
        {
            Element.Value = "first";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_choose_by_label_for()
        {
            Element.Value = "label2";
            Assert.That(Element.Value, Is.EqualTo("label2 label2a"));
        }

        [Test]
        public void Should_choose_by_multiple_labels_for()
        {
            Element.Value = "label2a";
            Assert.That(Element.Value, Is.EqualTo("label2 label2a"));
        }

        [Test]
        public void Value_should_combine_labels_for_and_parent_label()
        {
            Element.Value = "label3";
            Assert.That(Element.Value, Is.EqualTo("label3 third label3a"));
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
            Assert.That(Element.Options, Is.EqualTo(new List<string> { "first", "label2 label2a", "label3 third label3a", "value4", "on"}));
        }

        [TestFixture]
        public class NoRadioButtonsTest : ElementTest<TestPage<Radio>, Radio>
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
