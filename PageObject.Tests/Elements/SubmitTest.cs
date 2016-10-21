using System.Collections.Generic;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class SubmitTest : HtmlElementTest<TestPage<Submit>, Submit>
    {
        private Submit Submit => Element;

        protected override string ElementHtml => @"<input name='name' type='submit'>";

        [Test]
        public void Should_be_able_to_click()
        {
            Submit.Click();
        }
     }
}
