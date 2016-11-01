using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class ElementTest<TP, TE> : BaseElementTest<TP, TE> where TP : TestPage<TE> where TE : Element
    {
        private Element Element => base.Element;

        [Test]
        public virtual void Should_provide_access_to_coypu_element()
        {
            Assert.That(Element.CoypuElement, Is.Not.Null);
        }

        [Test]
        public virtual void Should_provide_text()
        {
            Assert.That(Element.Text, Is.EqualTo(Element.CoypuElement.Text));
        }

        [Test]
        public virtual void Should_provide_title()
        {
            Assert.That(Element.Title, Is.EqualTo(Element.CoypuElement.Title));
        }
    }
}
