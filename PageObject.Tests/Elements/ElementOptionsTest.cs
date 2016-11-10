using Coypu;
using NUnit.Framework;
using PageObject.Elements;
using Text = PageObject.Elements.Text;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class ElementOptionsTest : ElementTest<TestPageWithOptions<Text>, Text>
    {
        protected override string ElementHtml => @"
            <input name='name' id='first'  type='text'>
            <input name='name' id='second' type='text'>
        ";

        [Test]
        [ExpectedException(typeof(AmbiguousException))]
        public void Should_find_using_provided_options()
        {
            var x = Page.SingleExact.CoypuElement.Id;
        }
    }

    public class TestPageWithOptions<T> : TestPage<T> where T: BaseElement
    {
        public TestPageWithOptions(PageSession session) : base(session)
        {
        }

        [Element("name")]
        public T First => Element<T>();

        [Element("name")]
        public T SingleExact => Element<T>(Options.SingleExact);
    }
}
