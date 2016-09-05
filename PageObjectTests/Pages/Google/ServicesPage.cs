using System;
using PageObject;

namespace PageObjectTests.Pages.Google
{
    public static class ServicesPage
    {
        public static Uri Uri = new Uri("http://www.google.com/services");
    }

    [Page("http://www.google.com/services")]
    public class ServicesPageWithPathOnly : Page
    {
        public ServicesPageWithPathOnly(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(GooglePage), "services")]
    public class ServicesPageWithParentPageAndPath : Page
    {
        public ServicesPageWithParentPageAndPath(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(ServicesPageWithPathOnly))]
    public class ServicesPageWithParentPageAndMissingPath : Page
    {
        public ServicesPageWithParentPageAndMissingPath(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(ServicesPageWithPathOnly), "")]
    public class ServicesPageWithParentPageAndEmptyPath : Page
    {
        public ServicesPageWithParentPageAndEmptyPath(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(ServicesPageWithPathOnly), null)]
    public class ServicesPageWithParentPageAndNullPath : Page
    {
        public ServicesPageWithParentPageAndNullPath(PageSession session = null) : base(session)
        {
        }
    }
}