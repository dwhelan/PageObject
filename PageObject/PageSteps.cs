using BoDi;
using Coypu.NUnit.Matchers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace PageObject
{
    [Binding]
    public class PageSteps : Base
    {
        public PageSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        [Given(@"I browse to the ""(.*)""")]
        public void GivenIBrowseToThe(string pageName)
        {
            PageFor(pageName).Visit();
        }

        [Then(@"I should be on the ""(.*)""")]
        public void ThenIShouldBeOnThe(string pageName)
        {
            var expectedPage = PageFactory.Instance.Create(pageName);
            Assert.Contains(Browser.Location.Host, expectedPage.HostAliases);
            Assert.That(Browser.Location.LocalPath, Is.EqualTo(expectedPage.Path));
            // TODO also check SSL and port
        }

        [Then(@"the page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string title)
        {
            Assert.That(Page.Title, Is.EqualTo(title));
        }

        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string text)
        {
            Assert.That(Browser, Shows.Content(text));
        }

        // You should create a page object with this behaviour rather than using the browser directly
        [When(@"I click the ""(.*)"" button")]
        public void WhenIClickTheButton(string search)
        {
            Browser.ClickButton(search);
        }

        // You should create a page object with this behaviour rather than using the browser directly
        [When(@"I click the ""(.*)"" link")]
        public void WhenIClickTheLink(string name)
        {
            Browser.ClickLink(name);
        }
    }
}
