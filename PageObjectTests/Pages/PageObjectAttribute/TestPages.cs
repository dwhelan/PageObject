using System;
using PageObject;

namespace PageObjectTests.Pages.PageObjectAttribute
{
    public static class Constants
    {
        public const string Url = BaseUrl + Path;
        public const string BaseUrl = "file:///";
        public const string Path = "something";
    }

    // The following pages classes should all be valid with a Uri.AbsoluteUri equal to Constants.Url

    [PageAt(Constants.Url)]
    public class BasePage : Page
    {
        public BasePage() : base(null) { }
    }


    // Valid page objects built with base page objects.

    [PageAt(typeof(BasePage), Constants.Path)]
    public class BasePageAndPath : Page
    {
        public BasePageAndPath() : base(null) { }
    }

    [PageAt(typeof(BasePage), null)]
    public class BasePageAndNullPath : Page
    {
        public BasePageAndNullPath() : base(null) {}
    }

    [PageAt(typeof(BasePage), "")]
    public class BasePageAndEmptyPath : Page
    {
        public BasePageAndEmptyPath() : base(null) { }
    }

    [PageAt(typeof(BasePage))]
    public class BasePageOnly : Page
    {
        public BasePageOnly() : base(null) {}
    }

    [PageAt((Type)null, Constants.Url)]
    public class NullBasePageAndPath : Page
    {
        public NullBasePageAndPath() : base(null) { }
    }


    // Valid page objects with base urls.

    [PageAt(Constants.BaseUrl, Constants.Path)]
    public class BaseUrlAndPath : Page
    {
        public BaseUrlAndPath() : base(null) { }
    }

    [PageAt(Constants.Url, null)]
    public class BaseUrlAndNullPath : Page
    {
        public BaseUrlAndNullPath() : base(null) { }
    }

    [PageAt(Constants.Url, "")]
    public class BaseUrlAndEmptyPath : Page
    {
        public BaseUrlAndEmptyPath() : base(null) { }
    }

    [PageAt((string)null, Constants.Url)]
    public class NullBaseUrlAndPath : Page
    {
        public NullBaseUrlAndPath() : base(null) { }
    }


    // The following invalid page classes should raise a PageObjectException when instantiated.

    [PageAt("invalid url")]
    public class InvalidUrl : Page
    {
        public InvalidUrl() : base(null) {}
    }

    [PageAt(typeof(string), null)]
    public class BaseThatIsNotAPage : Page
    {
        public BaseThatIsNotAPage() : base(null) { }
    }

    [PageAt("invalid url", "path")]
    public class BaseThatIsAnInvalidUrl : Page
    {
        public BaseThatIsAnInvalidUrl() : base(null) { }
    }

    [PageAt(typeof(SelfReferencingPage), null)]
    public class SelfReferencingPage : Page
    {
        public SelfReferencingPage() : base(null) { }
    }

    // Circular references: CircularReference1a => CircularReference1b => CircularReference1a

    [PageAt(typeof(CircularReference1B), null)]
    public class CircularReference1A : Page
    {
        public CircularReference1A() : base(null) { }
    }

    [PageAt(typeof(CircularReference1A), null)]
    public class CircularReference1B : Page
    {
        public CircularReference1B() : base(null) { }
    }

    // Circular references: CircularReference2a => CircularReference2b => CircularReference2c => CircularReference2a

    [PageAt(typeof(CircularReference2B), null)]
    public class CircularReference2A : Page
    {
        public CircularReference2A() : base(null) { }
    }

    [PageAt(typeof(CircularReference2C), null)]
    public class CircularReference2B : Page
    {
        public CircularReference2B() : base(null) { }
    }

    [PageAt(typeof(CircularReference2A), null)]
    public class CircularReference2C : Page
    {
        public CircularReference2C() : base(null) { }
    }
}
