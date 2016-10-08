﻿using System;
using System.IO;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using Element = PageObject.Elements.Element;

namespace PageObject.Tests.Elements
{
    public static class TestConstants
    {
        internal const string HtmlFileName = "ElementTestPage.html";
    }

    public abstract class Test<TP, TE> where TP : TestPage<TE> where TE : Element
    {
        protected TE Element => page.Element;
        protected abstract string ElementHtml { get; }

        private PageSession session;
        private TP page;

        [TestFixtureSetUp]
        public void VisitPage()
        {
            session = CreateSession();
            page = CreatePage(session);
            WriteHtml();
            page.Visit();
        }

        private void WriteHtml()
        {
            using (var writer = new StreamWriter(TestConstants.HtmlFileName))
            {
                writer.WriteLine(ElementHtml);
            }
        }

        private static PageSession CreateSession()
        {
            var configuration = new SessionConfiguration { Browser = Browser.PhantomJS };
            return new PageSession(configuration);
        }

        private static TP CreatePage(PageSession session)
        {
            return (TP)Activator.CreateInstance(typeof(TP), session);
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            session?.Dispose();
        }
    }
}