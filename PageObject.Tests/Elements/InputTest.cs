using System;
using System.IO;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class InputTest<TP, TE> : HtmlElementTest<TP, TE> where TP : TestPage<TE> where TE : Input
    {
        [Test]
        public void Should_be_enabled()
        {
            Assert.That(Element.Enabled, Is.True);
            Assert.That(Element.Disabled, Is.False);
        }

        [Test]
        [Ignore]
        public void Should_be_disabled()
        {
            Session.Browser.ExecuteScript("document.getElementsByName('name')[0].disabled=true");
            Assert.That(Element.Enabled, Is.False);
            Assert.That(Element.Disabled, Is.True);
        }
    }
}
