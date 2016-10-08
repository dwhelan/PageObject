using NUnit.Framework;
using Text = PageObject.Elements.Text;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class TextTest : Test<TestPage<Text>, Text>
    {
        protected override string ElementHtml => "<input type='text' name='name' value='initial password'>";

        [Test]
        public void Should_get_value()
        {
            Assert.That(Element.Value, Is.EqualTo("initial password"));
        }

        [Test]
        public void Should_set_value()
        {
            Element.Value = "new password";
            Assert.That(Element.Value, Is.EqualTo("new password"));
        }
    }
}
