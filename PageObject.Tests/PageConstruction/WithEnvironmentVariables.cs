using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstruction
{
    [TestFixture]
    public class WithEnvironmentVariables : BaseTest
    {
        // Use environment variables with angle brackets to reduce liklihood of affecting
        // the environment variables that could be used elsewhere.

        [TestFixtureSetUp]
        public void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable("<host>", "host");
            Environment.SetEnvironmentVariable("<path>", "path");
        }

        [TestFixtureTearDown]
        public void RemoveEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable("<host>", null);
            Environment.SetEnvironmentVariable("<path>", null);
        }

        [TestCase(typeof(PageWithEnvironmentVariableInPath))]
        [TestCase(typeof(PageWithEnvironmentVariableInUrl))]
        [TestCase(typeof(PageWithEnvironmentVariableInUrlAndPath))]
        public void Should_expand_environment_variable_in_path(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Url, Is.EqualTo("http://host/path"));
        }
            [PageAt("http://${<host>}/${<path>}")]
            private class PageWithEnvironmentVariableInPath : Page
            {
                public PageWithEnvironmentVariableInPath(PageSession session) : base(session) { }
            }

            [PageAt("http://${<host>}/${<path>}", "")]
            private class PageWithEnvironmentVariableInUrl : Page
            {
                public PageWithEnvironmentVariableInUrl(PageSession session) : base(session) { }
            }

            [PageAt("http://${<host>}", "${<path>}")]
            private class PageWithEnvironmentVariableInUrlAndPath : Page
            {
                public PageWithEnvironmentVariableInUrlAndPath(PageSession session) : base(session) { }
            }
    }
}
