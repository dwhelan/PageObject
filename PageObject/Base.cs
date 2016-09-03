using BoDi;

namespace PageObject
{
    // TODO: Could we use a class attribute [Page] rather than subclassing
    public abstract class Base
    {
        protected readonly IObjectContainer ObjectContainer;
        protected readonly PageSession Browser;

        protected Page Page
        {
            get { return Browser.Page; }
            set { Browser.Page = value; }
        }

        protected Base(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;
            Browser = objectContainer.Resolve<PageSession>();
        }

        protected Page PageFor(string pageName)
        {
            return Page = PageFactory.Instance.Create(pageName, Browser);
        }
    }
}