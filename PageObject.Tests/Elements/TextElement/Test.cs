using System;
using NUnit.Framework;
using Text = PageObject.Elements.Text;

namespace PageObject.Tests.Elements.TextElement
{
    [TestFixture]
    public class Test : ElementTest
    {
        protected override Type PageType => typeof(TestPage);
        private Text Element => ((TestPage) Page).Element;

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
