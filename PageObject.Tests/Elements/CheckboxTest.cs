using System.Collections.Generic;
using NUnit.Framework;
using static OpenQA.Selenium.Keys;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class CheckboxTest : InputTest<TestPage<Checkbox>, Checkbox>
    {
        private Checkbox Checkbox => Element;

        protected override string ElementHtml => @"<input type='checkbox' name='name'>";

        [Test]
        public void Should_be_able_to_select()
        {
            Checkbox.Select();
            Assert.That(Checkbox.Value, Is.True);
        }

        [Test]
        public void Select_should_be_idempotent()
        {
            Checkbox.Select();
            Checkbox.Select();
            Assert.That(Checkbox.Value, Is.True);
        }

        [Test]
        public void Should_be_able_to_deselect()
        {
            Checkbox.Deselect();
            Assert.That(Checkbox.Value, Is.False);
        }

        [Test]
        public void Deselect_should_be_idempotent()
        {
            Checkbox.Deselect();
            Checkbox.Deselect();
            Assert.That(Checkbox.Value, Is.False);
        }

        [Test]
        public void Should_be_able_to_check_by_setting_value_to_true()
        {
            Checkbox.Value = true;
            Assert.That(Checkbox.Value, Is.True);
        }

        [Test]
        public void Should_be_able_to_uncheck_by_setting_value_to_false()
        {
            Checkbox.Value = false;
            Assert.That(Checkbox.Value, Is.False);
        }

        [Test]
        public void Should_be_able_to_check_by_clicking()
        {
            Checkbox.Click();
            Assert.That(Checkbox.Value, Is.True);
        }

        [Test]
        public void Options_should_return_list_of_true_and_false()
        {
            Assert.That(Checkbox.Options, Is.EqualTo(new List<bool> { true, false }));
        }

        [Test]
        public void Click_should_toggle_state()
        {
            Checkbox.Click();
            Checkbox.Click();
            Assert.That(Checkbox.Value, Is.False);
        }

        [Test]
        public void Sendkeys_of_Space_should_toggle()
        {
            Checkbox.Click();
            Checkbox.SendKeys(Home);
            Assert.That(Checkbox.Value, Is.True);
        }

    }
}
