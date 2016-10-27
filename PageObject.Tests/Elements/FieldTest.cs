using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class FieldTest<TP, TE> : ElementTest<TP, TE> where TP : TestPage<TE> where TE : Field
    {
        private Field Field => Element;

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Field.Enabled, Is.True);
            Assert.That(Field.Disabled, Is.False);
        }

        [Test]
        public void Should_be_disabled()
        {
            Session.Browser.ExecuteScript("document.getElementsByName('name')[0].disabled=true");
            Assert.That(Field.Enabled, Is.False);
            Assert.That(Field.Disabled, Is.True);
        }
    }
}
