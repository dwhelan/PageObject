using System;
using System.Reflection;
using NUnit.Framework;

namespace PageObject.Tests.PageAttributes
{
    public abstract class BaseTest
    {
        protected static Uri Uri = new Uri(Url);

        protected const string Url = BaseUrl + Path;
        protected const string BaseUrl = "file:///";
        protected const string Path = "something";

        protected Exception AssertPageCreationThrows(Type pageClass, string regEx)
        {
            var x = Assert.Throws<PageObjectException>(() => CreatePage(pageClass));
            StringAssert.IsMatch(regEx, x.Message);
            return x;
        }

        protected void AssertThatPageCanBeCreated(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Url));
        }

        protected Page CreatePage(Type pageClass)
        {
            try
            {
                return (Page) Activator.CreateInstance(pageClass);
            }
            catch (TargetInvocationException x)
            {
                throw x.InnerException;
            }
        }
    }
}