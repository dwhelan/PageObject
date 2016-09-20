using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstruction
{
    [TestFixture]
    public class WithEnvironmentVariables : BaseTest
    {
        private static readonly string CurrentDirectoryPath = Environment.CurrentDirectory.Replace('\\', '/');

        [TestCase(typeof(PageWithEnvironmentVariableInPath))]
        [TestCase(typeof(PageWithEnvironmentVariableInUrl))]
        [TestCase(typeof(PageWithEnvironmentVariableInUrlAndPath))]
        public void Should_expand_environment_variable_in_path(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Url, Is.EqualTo(string.Format("file:///{0}/", CurrentDirectoryPath)));
        }
            [PageAt("file:///{cd}/")]
            private class PageWithEnvironmentVariableInPath : Page
            {
                public PageWithEnvironmentVariableInPath(PageSession session) : base(session) { }
            }

            [PageAt("file:///{CD}/", "")]
            private class PageWithEnvironmentVariableInUrl : Page
            {
                public PageWithEnvironmentVariableInUrl(PageSession session) : base(session) { }
            }

            [PageAt("file:///", "{CD}/")]
            private class PageWithEnvironmentVariableInUrlAndPath : Page
            {
                public PageWithEnvironmentVariableInUrlAndPath(PageSession session) : base(session) { }
            }
    }
}
