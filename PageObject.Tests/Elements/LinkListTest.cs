using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class LinkListTest : BaseElementTest<TestPage<Link>, Link>
    {
        private ElementList<Link> List => ElementList;

        protected override string ElementHtml => @"
            <a href='http://example.com/1'>list[]</a>
            <a href='http://example.com/2'>list[]</a>
        ";

        [Test]
        public void Should_get_first_link()
        {
            Assert.That(List[0].CoypuElement["href"], Is.EqualTo("http://example.com/1"));
        }

        [Test]
        public void Should_get_second_value()
        {
            Assert.That(List[1].CoypuElement["href"], Is.EqualTo("http://example.com/2"));
        }
    }
}
