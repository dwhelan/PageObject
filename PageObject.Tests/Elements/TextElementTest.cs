using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class TextElementTest : ElementTest<TestPage<TextElement>, TextElement>
    {
        protected override string ElementHtml => "<input type='text' name='name' value='initial'>text</input>";

        [Test]
        public void Should_get_initial_value()
        {
            Assert.That(Element.Value, Is.EqualTo("initial"));
        }

        [Test]
        public void Should_set_value()
        {
            Element.Value = "new";
            Assert.That(Element.Value, Is.EqualTo("new"));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.EqualTo("text"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }

        [TestFixture]
        public class DisabledTextElementTest : ElementTest<TestPage<TextElement>, TextElement>
        {
            protected override string ElementHtml => "<input type='text' name='name' disabled>";

            [Test]
            public void Should_be_disabled()
            {
                Assert.That(Element.Enabled, Is.False);
                Assert.That(Element.Disabled, Is.True);
            }
        }
    }
}
