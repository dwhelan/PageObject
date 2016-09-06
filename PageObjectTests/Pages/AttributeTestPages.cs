using System;
using PageObject;

namespace PageObjectTests.Pages
{
    public static class Constants
    {
        public const string Url = BaseUrl + Path;
        public const string BaseUrl = "file:///";
        public const string Path = "something";
    }

    // The following pages classes should all be valid with a Uri.AbsoluteUri equal to Constants.Url

    [PageObject(Constants.Url)]
    public class WithPathOnly : Page
    {
        public WithPathOnly() : base(null) { }
    }

    [PageObject(Constants.Url, (Type) null)]
    public class WithPathAndNullBasePage : Page
    {
        public WithPathAndNullBasePage() : base(null) { }
    }

    [PageObject(Constants.Url, (string)null)]
    public class WithPathAndNullBaseUrl : Page
    {
        public WithPathAndNullBaseUrl() : base(null) { }
    }

    // Valid page objects built with base page objects.

    [PageObject(Constants.Path, typeof(WithPathOnly))]
    public class WithPathAndBasePage : Page
    {
        public WithPathAndBasePage() : base(null) { }
    }

    [PageObject(null, typeof(WithPathOnly))]
    public class WithNullPathAndBasePage : Page
    {
        public WithNullPathAndBasePage() : base(null) {}
    }

    [PageObject("", typeof(WithPathOnly))]
    public class WithEmptyPathAndBasePage : Page
    {
        public WithEmptyPathAndBasePage() : base(null) {}
    }

    // Valid page objects with base urls.

    [PageObject(Constants.Path, Constants.BaseUrl)]
    public class WithPathAndBaseUrl : Page
    {
        public WithPathAndBaseUrl() : base(null) { }
    }

    [PageObject(null, Constants.Url)]
    public class WithNullPathAndBaseUrl : Page
    {
        public WithNullPathAndBaseUrl() : base(null) { }
    }

    [PageObject("", Constants.Url)]
    public class WithEmptyPathAndBaseUrl : Page
    {
        public WithEmptyPathAndBaseUrl() : base(null) { }
    }


    // The following invalid page classes should raise a PageObjectException when instantiated.

    [PageObject("invalid url")]
    public class WithInvalidUrl : Page
    {
        public WithInvalidUrl() : base(null) {}
    }

    [PageObject(null, typeof(string))]
    public class WithBaseThatIsNotAPage : Page
    {
        public WithBaseThatIsNotAPage() : base(null) { }
    }

    [PageObject(null, "invalid url")]
    public class WithBaseThatIsNotAValidUrl : Page
    {
        public WithBaseThatIsNotAValidUrl() : base(null) { }
    }
}
