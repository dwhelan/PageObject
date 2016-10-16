﻿using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class MultiSelectTest : InputTest<TestPage<MultiSelect>, MultiSelect>
    {
        protected override string ElementHtml =>
            @"<select name='name' multiple=''>
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
        public void Setting_value_should_allow_single_option_to_be_set()
        {
            var selected = new List<string> { "First" };
            Element.Value = selected;
            Assert.That(Element.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_value_should_allow_multiple_options_to_be_set()
        {
            var selected = new List<string> { "First", "Second" };
            Element.Value = selected;
            Assert.That(Element.Value, Is.EqualTo(selected));
        }

        [Test]
        public void Setting_empty_value_should_deselect_all_selections()
        {
            Element.Value = new List<string> { "First", "Second" };
            Element.Value = new List<string>();
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Select_should_select_a_previously_unselected_option()
        {
            Element.Select("First");
            Assert.That(Element.Value, Is.EqualTo(new List<string> { "First" }));
        }

        [Test]
        public void Select_should_be_idempotent()
        {
            Element.Select("First");
            Element.Select("First");
            Assert.That(Element.Value, Has.Exactly(1).EqualTo("First"));
        }

        [Test]
        public void Deselect_should_select_a_previously_unselected_option()
        {
            Element.Select("First");
            Element.Deselect("First");
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Deselect_should_be_idempotent()
        {
            Element.Select("First");
            Element.Deselect("First");
            Element.Deselect("First");
            Assert.That(Element.Value, Is.Empty);
        }

        [Test]
        public void Options_should_return_all_options()
        {
            Assert.That(Element.Options, Is.EqualTo(new List<string> { "First", "Second", "Third"}));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.StringMatching(@"^\s*First\s*Second\s*Third\s*$"));
        }
    }
}