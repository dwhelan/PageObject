using System;

namespace PageObject.SpecFlow.Tests.Pages.File
{
    internal class HomePage : Page
    {
        internal new static Uri Uri => new Uri(Root.Uri, "Home.html");
        internal new static string Url => Uri.AbsoluteUri;

        public HomePage(PageSession session) : base(session, Uri)
        {
        }
    }
}