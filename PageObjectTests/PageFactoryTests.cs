using System;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using PageObject;

namespace PageObjectTests
{
    public abstract class AbstractTestPage : Page
    {
        protected AbstractTestPage(PageSession session, string path) : base(session, "test.com", path, false)
        {
        }
    }

    public class TestPage : AbstractTestPage
    {
        public TestPage(PageSession session) : base(session, "path")
        {
        }
    }

    public class Test2Page : AbstractTestPage
    {
        public Test2Page(PageSession session) : base(session, "path2")
        {
        }
    }

    [TestFixture]
    public class PageFactoryTests
    {
        [Test]
        public void Should_auto_register_sites()
        {
            Assert.That(PageFactory.Instance.PageClassFor("TestPage"), Is.EqualTo(typeof(TestPage)));
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
            Assert.That(x.Message, Is.StringContaining("PageObjectTests.TestPage"));
            Assert.That(x.Message, Is.StringContaining("PageObjectTests.Test2Page"));
        }

        [TestCase("TestPage")]
        [TestCase("PageObjectTests.TestPage")]
        public void Find_should_locate_with_partial_or_full_class_name_match(string siteName)
        {
            Assert.That(PageFactory.Instance.PageClassFor(siteName), Is.EqualTo(typeof(TestPage)));
        }

        [TestCase(" Test Page ")]
        [TestCase("Page Object Tests Test Page")]
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
