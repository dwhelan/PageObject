using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class RadioElementTest : ElementTest<TestPage<RadioElement>, RadioElement>
    {
        protected override string ElementHtml =>
            @"<input type='radio' name='test' value='name'/>
              <input type='radio' name='test' value='second'/>";

        [Test]
        public void Should_be_able_to_choose()
        {
            Element.Choose();
            Assert.That(Element.Selected, Is.True);
        }

        [Test]
        public void Should_be_able_to_unchoose_by_choosing_another_radio_button()
        {
            Element.Choose();
            Element.Browser.Choose("second");
            Assert.That(Element.Selected, Is.False);
        }

        [Test]
        [Explicit("Cannot get text from radio button")]
        public void Should_provide_text()
        {
            Assert.That(Element.Text, Is.EqualTo("first"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }

        [TestFixture]
        public class DisabledRadioElementTest : ElementTest<TestPage<RadioElement>, RadioElement>
        {
            protected override string ElementHtml => @"<input type='radio' name='test' value='name' disabled/>";

            [Test]
            public void Should_be_disabled()
            {
                Assert.That(Element.Enabled, Is.False);
                Assert.That(Element.Disabled, Is.True);
            }
        }
    }
}
