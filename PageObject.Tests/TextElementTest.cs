using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests
{
    [TestFixture]
    public class TextElementTest
    {
        private PageSession session;
        private LoginPage page;

        [PageAt("file:///${cd}/../../Pages/File/Login.html")]
        internal class LoginPage : Page
        {
            public LoginPage(PageSession session) : base(session)
            {
            }

            [PageElement("userName")]
            public string UserName
            {
                get { return FindField().Value; }
                set { FillIn().With(value); }
            }

            [PageElement("password")]
            public string Password
            {
                get { return FindField().Value; }
                set { FillIn().With(value); }
            }
        }

        [TestFixtureSetUp]
        public void VisitThePage()
        {
            var configuration = new SessionConfiguration { Browser = Browser.PhantomJS };
            session = new PageSession(configuration);
            page = new LoginPage(session);
            page.Visit();
        }

        [Test]
        public void Text_property_getter_should_return_the_text_element_value()
        {
            Assert.That(page.UserName, Is.EqualTo("user"));
        }

        [Test]
        public void Text_property_setter_should_set_the_text_element_value()
        {
            page.UserName = "another user";
            Assert.That(page.UserName, Is.EqualTo("another user"));
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}
