using NUnit.Framework;
using PageObject.Elements;
using Text = PageObject.Elements.Text;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class TextListTest : BaseElementTest<TestPage<Text>, Text>
    {
        private ElementList<Text> List => ElementList;

        protected override string ElementHtml => @"
            <input name='list[]' value='first' type='text'>
            <input name='list[]' value='second' type='text'>
        ";

        [Test]
        public void Should_get_first_value()
        {
            Assert.That(List[0].Value, Is.EqualTo("first"));
        }

        [Test]
        public void Should_get_second_value()
        {
            Assert.That(List[1].Value, Is.EqualTo("second"));
        }
    }
}
