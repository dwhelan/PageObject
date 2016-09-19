using NUnit.Framework;

namespace PageObject.Tests.PageConstruction
{
    [TestFixture]
    public class WithUrl : BaseTest
    {
        [Test]
        public void Should_support_a_base_url()
        {
            AssertThatPageCanBeCreated(typeof(BaseUrlOnly));
        }

            [PageAt(BaseTest.Url)]
            private class BaseUrlOnly : Page
            {
                public BaseUrlOnly(PageSession session = null) : base(session) { }
            }
     }
}
