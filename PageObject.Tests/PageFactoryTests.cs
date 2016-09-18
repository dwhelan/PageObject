using System;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;

namespace PageObject.Tests
{
    [TestFixture]
    public class PageFactoryTests
    {
        [PageAt("http://www.test.com")]
        public abstract class AbstractTestPage : Page
        {
            protected AbstractTestPage(PageSession session) : base(session) {}
        }

        [PageAt("http://www.test.com", "path")]
        public class TestPage : AbstractTestPage
        {
            public TestPage(PageSession session) : base(session) {}
        }

        [PageAt("http://www.test.com", "path2")]
        public class Test2Page : AbstractTestPage
        {
            public Test2Page(PageSession session) : base(session) {}
        }

        [Test]
        public void Should_auto_register_sites()
        {
            Assert.That(PageFactory.Instance.PageClassFor("PageFactoryTestsTestPage"), Is.EqualTo(typeof(TestPage)));
        }

        [Test]
        public void Should_not_load_abstract_site_classes()
        {
            Assert.That(PageFactory.Instance.Contains(typeof(AbstractTestPage)), Is.False);
        }

        [Test]
        public void Find_should_throw_if_site_class_cannot_be_found()
        {
            Assert.Throws<ArgumentException>(() => PageFactory.Instance.PageClassFor("This name should not be found anywhere"));
        }

        [Test]
        public void Find_should_throw_if_multiple_site_classes_found()
        {
            var x = Assert.Throws<ArgumentException>(() => PageFactory.Instance.PageClassFor("Page"));
            Assert.That(x.Message, Is.StringContaining("PageObject.Tests.PageFactoryTests+TestPage"));
            Assert.That(x.Message, Is.StringContaining("PageObject.Tests.PageFactoryTests+Test2Page"));
        }

        [TestCase("PageFactoryTestsTestPage")]
        [TestCase("FactoryTestsTestPage")]
        public void Find_should_locate_with_partial_or_full_class_name_match(string siteName)
        {
            Assert.That(PageFactory.Instance.PageClassFor(siteName), Is.EqualTo(typeof(TestPage)));
        }

        [TestCase("Page Factory Tests Test Page")]
        [TestCase(@"!@#$%^&*()+{}|:"";'<>?,./ Page Factory Tests Test Page ")]
        public void Find_remove_punctuation_from_site_names(string siteName)
        {
            try
            {
                Assert.That(PageFactory.Instance.PageClassFor(siteName), Is.EqualTo(typeof(TestPage)));
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var item in ex.LoaderExceptions)
                {
                    MessageBox.Show(item.Message);
                }
            }
        }
    }
}
