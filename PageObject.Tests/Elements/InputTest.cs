using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class InputTest<TP, TE> : ElementTest<TP, TE> where TP : TestPage<TE> where TE : Input
    {
        private Input Input => Element;

        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Input.Enabled, Is.True);
            Assert.That(Input.Disabled, Is.False);
        }

        [Test]
        public void Should_be_disabled()
        {
            Session.Browser.ExecuteScript("document.getElementsByName('name')[0].disabled=true");
            Assert.That(Input.Enabled, Is.False);
            Assert.That(Input.Disabled, Is.True);
        }
    }
}
