using System.Collections.Generic;
using BoDi;
using Coypu;
using Coypu.Drivers;
using TechTalk.SpecFlow;

namespace PageObject
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer objectContainer;
        private PageSession session;
        private readonly List<Page> pages = new List<Page>();

        public Hooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void Before()
        {
            var configuration = new SessionConfiguration
            {
                // Uncomment the Browser you want
                //Browser = Browser.Firefox,
                Browser = Browser.Chrome,
                //Browser = Browser.InternetExplorer,
                //Browser = Browser.PhantomJS,
            };
            session = new PageSession(configuration);
            objectContainer.RegisterInstanceAs(session);
            objectContainer.RegisterInstanceAs(pages);
        }

        [AfterScenario]
        public void DisposeSites()
        {
            session.Dispose();
        }
    }
}