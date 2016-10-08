using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public abstract class BasePage<T> : Page where T : Element
    {
        protected BasePage(PageSession session) : base(session) {}

        public abstract T TestElement { get; }
    }
}
