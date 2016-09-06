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
    public class Base : Page
    {
        public Base() : base(null) { }
    }

    [PageObject((Type) null, Constants.Url)]
    public class WithNullBasePageAndPath : Page
    {
        public WithNullBasePageAndPath() : base(null) { }
    }

    [PageObject(Constants.Url, (string)null)]
    public class WithNullBaseUrlAndPath : Page
    {
        public WithNullBaseUrlAndPath() : base(null) { }
    }

    // Valid page objects built with base page objects.

    [PageObject(typeof(Base), Constants.Path)]
    public class WithBasePageAndPath : Page
    {
        public WithBasePageAndPath() : base(null) { }
    }

    [PageObject(typeof(Base), null)]
    public class WithBasePageAndNullPath : Page
    {
        public WithBasePageAndNullPath() : base(null) {}
    }

    [PageObject(typeof(Base), "")]
    public class BasePageAndEmptyPath : Page
    {
        public BasePageAndEmptyPath() : base(null) {}
    }

    // Valid page objects with base urls.

    [PageObject(Constants.Path, Constants.BaseUrl)]
    public class WithPathAndBaseUrl : Page
    {
        public WithPathAndBaseUrl() : base(null) { }
    }

    [PageObject((string) null, Constants.Url)]
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

    [PageObject(typeof(string), null)]
    public class BaseThatIsNotAPage : Page
    {
        public BaseThatIsNotAPage() : base(null) { }
    }

    [PageObject((Type) null, "invalid url")]
    public class WithBaseThatIsNotAValidUrl : Page
    {
        public WithBaseThatIsNotAValidUrl() : base(null) { }
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
