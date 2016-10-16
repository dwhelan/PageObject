﻿using System;
using System.IO;
using Coypu;
using Coypu.Drivers;
using NUnit.Framework;
using PageObject.Elements;

namespace PageObject.Tests.Elements
{
    public static class TestConstants
    {
        internal const string HtmlFileName = "ElementTestPage.html";
    }

    public abstract class ElementTest<TP, TE> where TP : TestPage<TE> where TE : PageObject.Elements.Element
    {
        protected virtual Browser Browser => Browser.PhantomJS;

        protected TE Element => page.Element;
        protected abstract string ElementHtml { get; }

        protected PageSession Session { get; private set; }
        private TP page;

        [TestFixtureSetUp]
        public void CreatePage()
        {
            Session = CreateSession();
            page = CreatePage(Session);
            WriteHtml();
        }

        [SetUp]
        public void VisitPage()
        {
            page.Visit();
        }

        private void WriteHtml()
        {
            using (var writer = new StreamWriter(TestConstants.HtmlFileName))
            {
                writer.WriteLine(ElementHtml);
            }
        }

        private PageSession CreateSession()
        {
            var configuration = new SessionConfiguration { Browser = Browser };
            return new PageSession(configuration);
        }

        private static TP CreatePage(PageSession session)
        {
            return (TP) Activator.CreateInstance(typeof(TP), session);
        }

        [TestFixtureTearDown]
        public void DisposeSession()
        {
            Session?.Dispose();
        }
    }
}
