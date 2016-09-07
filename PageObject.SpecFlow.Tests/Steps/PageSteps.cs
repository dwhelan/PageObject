using NUnit.Framework;
using TechTalk.SpecFlow;

namespace PageObject.Tests.Steps
{
    [Binding]
    public class PageSteps : PageObject.PageSteps
    {
        public PageSteps(PageSession session) : base(session)
        {
        }

        [When(@"I browse to the ""(.*)""")]
        public void WhenIBrowseToThe(string pageName)
        {
            Visit(pageName);
        }

        [Then(@"The page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string title)
        {
            Assert.That(Page.Title, Is.EqualTo(title));
        }
    }
}
