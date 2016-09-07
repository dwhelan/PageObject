namespace PageObject.SpecFlow
{
    public abstract class Steps
    {
        protected PageSession Session { get; }
        protected Page Page => Session.Page;

        protected Steps(PageSession session)
        {
            Session = session;
        }

        protected void Visit(string pageName)
        {
            PageFor(pageName).Visit();
        }

        private Page PageFor(string pageName)
        {
            return PageFactory.Instance.PageFor(pageName, Session);
        }
    }
}
