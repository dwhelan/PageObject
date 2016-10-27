using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SubmitButtonTest : ElementTest<TestPage<Button>, Button>
    {
        private Button Button => Element;

        protected override string ElementHtml => @"<input name='name' type='submit'>";

        [Test]
        public void Should_be_able_to_click()
        {
            Button.Click();
        }
    }

    [TestFixture]
    public class ButtonTest : ElementTest<TestPage<Button>, Button>
    {
        private Button Button => Element;

        protected override string ElementHtml => @"<button name='name'>";

        [Test]
        public void Should_be_able_to_click()
        {
            Button.Click();
        }
    }
}
