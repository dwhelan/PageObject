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
    public class BasePage : Page
    {
        public BasePage() : base(null) { }
    }

    [PageObject((Type) null, Constants.Url)]
    public class NullBasePageAndPath : Page
    {
        public NullBasePageAndPath() : base(null) { }
    }

    [PageObject((string) null, Constants.Url)]
    public class NullBaseUrlAndPath : Page
    {
        public NullBaseUrlAndPath() : base(null) { }
    }

    // Valid page objects built with base page objects.

    [PageObject(typeof(BasePage), Constants.Path)]
    public class BasePageAndPath : Page
    {
        public BasePageAndPath() : base(null) { }
    }

    [PageObject(typeof(BasePage), null)]
    public class BasePageAndNullPath : Page
    {
        public BasePageAndNullPath() : base(null) {}
    }

    [PageObject(typeof(BasePage), "")]
    public class BasePageAndEmptyPath : Page
    {
        public BasePageAndEmptyPath() : base(null) {}
    }

    // Valid page objects with base urls.

    [PageObject(Constants.BaseUrl, Constants.Path)]
    public class BaseUrlAndPath : Page
    {
        public BaseUrlAndPath() : base(null) { }
    }

    [PageObject(Constants.Url, (string) null)]
    public class BaseUrlAndNullPath : Page
    {
        public BaseUrlAndNullPath() : base(null) { }
    }

    [PageObject(Constants.Url, "")]
    public class BaseUrlAndEmptyPath : Page
    {
        public BaseUrlAndEmptyPath() : base(null) { }
    }


    // The following invalid page classes should raise a PageObjectException when instantiated.

    [PageObject("invalid url")]
    public class InvalidUrl : Page
    {
        public InvalidUrl() : base(null) {}
    }

    [PageObject(typeof(string), null)]
    public class BaseThatIsNotAPage : Page
    {
        public BaseThatIsNotAPage() : base(null) { }
    }

    [PageObject("invalid url", "path")]
    public class BaseThatIsAnInvalidUrl : Page
    {
        public BaseThatIsAnInvalidUrl() : base(null) { }
    }

    [PageObject(typeof(SelfReferencingPage), null)]
    public class SelfReferencingPage : Page
    {
        public SelfReferencingPage() : base(null) { }
    }

    // Circular references: CircularReference1a => CircularReference1b => CircularReference1a

    [PageObject(typeof(CircularReference1B), null)]
    public class CircularReference1A : Page
    {
        public CircularReference1A() : base(null) { }
    }

    [PageObject(typeof(CircularReference1A), null)]
    public class CircularReference1B : Page
    {
        public CircularReference1B() : base(null) { }
    }

    // Circular references: CircularReference2a => CircularReference2b => CircularReference2c => CircularReference2a

    [PageObject(typeof(CircularReference2B), null)]
    public class CircularReference2A : Page
    {
        public CircularReference2A() : base(null) { }
    }

    [PageObject(typeof(CircularReference2C), null)]
    public class CircularReference2B : Page
    {
        public CircularReference2B() : base(null) { }
    }

    [PageObject(typeof(CircularReference2A), null)]
    public class CircularReference2C : Page
    {
        public CircularReference2C() : base(null) { }
    }
}
