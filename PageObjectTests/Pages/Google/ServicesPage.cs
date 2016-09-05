using System;
using PageObject;

namespace PageObjectTests.Pages.Google
{
    public static class ServicesPage
    {
        public static Uri Uri = new Uri("http://www.google.com/services");
    }

    [Page(typeof(GooglePage), "services")]
    public class ServicesPageWithParentPageAndPath : Page
    {
        internal new static Uri Uri => new Uri("http://www.google.com/services");

        public ServicesPageWithParentPageAndPath(PageSession session = null) : base(session)
        {
        }
    }
 
    [Page("http://www.google.com/services")]
    public class ServicesPageWithUrlOnly : Page
    {
        public ServicesPageWithUrlOnly(PageSession session = null) : base(session)
        {
        }
    }
}