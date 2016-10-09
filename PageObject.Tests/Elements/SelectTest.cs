using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SelectTest : ElementTest<TestPage<Select>, Select>
    {
        protected override string ElementHtml =>
                                    @"<select name='name'>
                                        <option value='one' selected>First option</option>
                                        <option value='two'>Second option</option>
                                      </select>";

        [Test]
        public void Should_be_able_to_get_value()
        {
            Assert.That(Element.Value, Is.EqualTo("First option"));
        }

        [Test]
        public void Should_be_able_to_set_value()
        {
            Element.Value = "Second option";
            Assert.That(Element.Value, Is.EqualTo("Second option"));
        }

        [Test]
        public void Should_get_text()
        {
            Assert.That(Element.Text, Is.StringMatching(@"^\s*First option\s*Second option\s*$"));
        }

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }
    }

    [TestFixture]
    public class DisabledSelectTest : ElementTest<TestPage<Select>, Select>
    {
        protected override string ElementHtml =>
                                    @"<select name='name' disabled>
                                        <option value='one'>First option</option>
                                        <option value='two'>Second option</option>
                                      </select>";
        [Test]
        public void Should_be_disabled()
        {
            Assert.That(Element.Enabled, Is.False);
            Assert.That(Element.Disabled, Is.True);
        }
    }
}
