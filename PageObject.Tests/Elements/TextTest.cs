using NUnit.Framework;
using Text = PageObject.Elements.Text;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class TextTest : InputTest<TestPage<Text>, Text>
    {
        protected override string ElementHtml => "<input type='text' name='name' value='initial'>";
        protected Text Text => Element;

        [Test]
        public void Should_get_initial_value()
        {
            Assert.That(Text.Value, Is.EqualTo("initial"));
        }

        [Test]
        public void Should_set_value()
        {
            Text.Value = "new";
            Assert.That(Element.Value, Is.EqualTo("new"));
        }
    }
}
