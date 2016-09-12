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

        protected static T AssertInvokeThrows<T>(Func<Page> func) where T : Exception
        {
            var x = Assert.Throws<TargetInvocationException>(() => func.DynamicInvoke());
            Assert.That(x.InnerException, Is.AssignableTo(typeof(PageObjectException)));
            return (T) x.InnerException;
        }

        protected static T2 AssertInvokeThrows<T1, T2>(Func<Page> func) where T1 : Exception where T2 : Exception
        {
            var x = AssertInvokeThrows<T1>((func));
            Assert.That(x.InnerException, Is.AssignableTo(typeof(T2)));
            return (T2) x.InnerException;
        }

        protected static void AssertValidPage(Page page)
        {
            Assert.That(page.Url, Is.EqualTo(Url));
        }
    }
}
