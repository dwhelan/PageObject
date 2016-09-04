namespace PageObject
{
    public abstract class PageSteps
    {
        protected PageSession Session { get; }
        protected Page Page => Session.Page;

        protected PageSteps(PageSession session)
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
