using System;
using NUnit.Framework;

namespace PageObject.Tests.PageAttributes
{
    public abstract class BaseTest : Tests.BaseTest
    {
        protected void AssertThatPageCanBeCreated(Type pageClass)
        {
            var page = CreatePage(pageClass);
            Assert.That(page.Uri.AbsoluteUri, Is.EqualTo(Url));
        }
    }
}