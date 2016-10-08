using System;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements.SelectElement
{
    [TestFixture]
    public class Test : ElementTest<TestPage, Select>
    {
        protected override Type PageType => typeof(TestPage);

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
