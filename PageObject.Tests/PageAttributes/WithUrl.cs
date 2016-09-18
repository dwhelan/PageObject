using System;
using NUnit.Framework;

namespace PageObject.Tests.PageAttributes
{
    [TestFixture]
    public class WithUrl : BaseTest
    {
        [TestCase(typeof(BaseUrlOnly))]
        public void Should_support_a_base_url(Type pageClass)
        {
            AssertThatPageCanBeCreated(pageClass);
        }

            [PageAt(BaseTest.Url)]
            private class BaseUrlOnly : Page
            {
                public BaseUrlOnly(PageSession session = null) : base(session) { }
            }
     }
}
