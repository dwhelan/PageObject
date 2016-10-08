using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SelectTest : ElementTest<BasePage<Select>, Select>
    {
        protected override string ElementHtml =>
                                    @"<select name='name'>
                                        <option value='one'>First option</option>
                                        <option value='two'>Second option</option>
                                      </select>";

        [Test]
        public void Should_be_able_to_select_option_by_value()
        {
            Element.Selected = "one";
            Assert.That(Element.Value, Is.EqualTo("one"));
        }

        [Test]
        public void Should_be_able_to_select_option_by_text()
        {
            Element.Selected = "Second option";
            Assert.That(Element.Selected, Is.EqualTo("Second option"));
        }
    }
}
