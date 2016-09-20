using System;
using NUnit.Framework;

namespace PageObject.Tests.PageConstruction
{
    [TestFixture]
    public class WithEnvironmentVariables
    {
        [Test]
        public void Should_expand_cd_with_current_directory()
        {
            var page = new PageWithCd(null);
            var cd = Environment.CurrentDirectory.Replace('\\', '/');
            Assert.That(page.Url, Is.EqualTo(string.Format("file:///{0}/", cd)));
        }
            [PageAt("file:///{cd}/")]
            private class PageWithCd : Page
            {
                public PageWithCd(PageSession session) : base(session) { }
            }

        [Test]
        public void Should_expand_CD_with_current_directory()
        {
            var page = new PageWithCD(null);
            var cd = Environment.CurrentDirectory.Replace('\\', '/');
            Assert.That(page.Url, Is.EqualTo(string.Format("file:///{0}/", cd)));
        }
            [PageAt("file:///{CD}/")]
            private class PageWithCD : Page
            {
                public PageWithCD(PageSession session) : base(session) { }
            }
    }
}
