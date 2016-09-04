using BoDi;

namespace PageObject
{
    // TODO: Could we use a class attribute [Page] rather than subclassing
    public abstract class Base
    {
        protected readonly IObjectContainer ObjectContainer;
        protected readonly PageSession Session;

        protected Page Page
        {
            get { return Session.Page; }
            set { Session.Page = value; }
        }

        protected Base(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;
            Session = objectContainer.Resolve<PageSession>();
        }

        protected Page PageFor(string pageName)
        {
            return Page = PageFactory.Instance.Create(pageName, Session);
        }
    }
}