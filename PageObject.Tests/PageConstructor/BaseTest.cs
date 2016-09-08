using System;
using System.Reflection;
using NUnit.Framework;

namespace PageObject.Tests.PageConstructor
{
    public abstract class BaseTest
    {
        internal const string Url = BaseUrl + Path;
        internal const string BaseUrl = "file:///";
        internal const string Path = "something";

        protected static void AssertThrowsPageObjectException(Func<Page> func)
        {
            try
            {
                func.DynamicInvoke();
            }
            catch (TargetInvocationException x)
            {
                Assert.That(x.InnerException, Is.AssignableTo(typeof(PageObjectException)));
                Assert.That(x.InnerException.InnerException, Is.AssignableTo(typeof(UriFormatException)));
            }
        }

        protected static void AssertValidPage(Page page)
        {
            Assert.That(page.Url, Is.EqualTo(Url));
        }
    }
}