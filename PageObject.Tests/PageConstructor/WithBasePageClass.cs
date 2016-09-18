using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    [TestFixture]
    public class WithBasePageClass : BaseTest
    {
        [Test]
        public void Should_throw_if_page_attribute_missing()
        {
            AssertInvokeThrows<PageObjectException>(() => CreatePage(typeof(PageWithoutPageAtAttribute)), @"Missing .*attribute for.*PageWithoutPageAtAttribute");
        }

            private class PageWithoutPageAtAttribute : Page
            {
                public PageWithoutPageAtAttribute(PageSession session) : base(session) { }
            }
    }
}
