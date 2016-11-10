using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public class CoypuElementTest<TP, TE> : ElementTest<TP, TE> where TP : TestPage<TE> where TE : Element
    {
        protected override string ElementHtml => "<textarea name='name'>initial</textarea>";

        [Test]
        public virtual void Should_provide_access_to_coypu_element()
        {
            Assert.That(CoypuElement, Is.Not.Null);
        }
 
        [Test]
        public virtual void Should_provide_text()
        {
            Assert.That(Element.Text, Is.EqualTo(CoypuElement.Text));
        }

        [Test]
        public virtual void Should_provide_title()
        {
            Assert.That(Element.Title, Is.EqualTo(CoypuElement.Title));
        }
    }
}
