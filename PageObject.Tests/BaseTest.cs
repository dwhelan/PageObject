using System;
using System.Reflection;
using NUnit.Framework;

namespace PageObject.Tests
{
    public abstract class BaseTest
    {
        protected static Uri Uri = new Uri(Url);

        internal const string Url = BaseUrl + Path;
        internal const string BaseUrl = "file:///";
        internal const string Path = "something";

        protected void AssertThatPageCanBeCreated(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Url));
        }

        protected T AssertInvokeThrows<T>(Func<Page> func, string messageRegEx) where T : Exception
        {
            var x = AssertInvokeThrows<T>(func);
            StringAssert.IsMatch(messageRegEx, x.Message);
            return x;
        }

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

        protected Page CreatePage(Type pageClass)
        {
            try
            {
                return PageFactory.Instance.PageFor(pageClass);
            }
            catch (TargetInvocationException x)
            {
                throw x.InnerException;
            }
        }
    }
}
