using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class LinkTest : ElementTest<TestPage<Link>, Link>
    {
        private Link Button => Element;

        protected override string ElementHtml => $"<a href='{Page.Uri}'>name</a>";

        [Test]
        public void Should_be_able_to_click()
        {
            Button.Click();
        }
    }
}
