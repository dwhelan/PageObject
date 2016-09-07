using NUnit.Framework;
using TechTalk.SpecFlow;

namespace PageObject.SpecFlow.Tests.Steps
{
    [Binding]
    public class Steps : SpecFlow.Steps
    {
        public Steps(PageSession session) : base(session)
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
