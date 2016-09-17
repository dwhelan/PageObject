using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    [TestFixture]
    public class WithBasePageClass : BaseTest
    {
        [PageAt(BaseTest.Url)]
        private class BasePage : Page
        {
            public BasePage() : base(null) { }
            public BasePage(PageSession session) : base(session) { }
        }
    }
}
