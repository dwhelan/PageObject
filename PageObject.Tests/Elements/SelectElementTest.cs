﻿using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SelectElementTest : InputTest<TestPage<SelectElement>, SelectElement>
    {
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
            Element.Select("first");
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Setting_value_should_select_option()
        {
            Element.Value = "first";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_keep_option_selected_when_selected_multiple_times()
        {
            Element.Value = "first";
            Element.Value = "first";
            Assert.That(Element.Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.StringMatching(@"^\s*first\s*second\s*third\s*$"));
        }

        [Test]
        [Ignore]
        public void Options_should_return_all_values()
        {
            Assert.That(Element.Options, Is.EqualTo(new List<string> { "first", "second", "third" }));
        }
    }
}