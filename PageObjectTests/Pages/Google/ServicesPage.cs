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
    public class ServicesPageWithParentAndPath : Page
    {
        public ServicesPageWithParentAndPath(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(ServicesPageWithPathOnly))]
    public class ServicesPageWithParentAndMissingPath : Page
    {
        public ServicesPageWithParentAndMissingPath(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(ServicesPageWithPathOnly), "")]
    public class ServicesPageWithParentAndEmptyPath : Page
    {
        public ServicesPageWithParentAndEmptyPath(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(ServicesPageWithPathOnly), null)]
    public class ServicesPageWithParentAndNullPath : Page
    {
        public ServicesPageWithParentAndNullPath(PageSession session = null) : base(session)
        {
        }
    }

    [Page(typeof(string))]
    public class ServicesPageWithParentThatIsNotAPage : Page
    {
        public ServicesPageWithParentThatIsNotAPage(PageSession session = null) : base(session)
        {
        }
    }

}