using PageObject;

namespace PageObjectTests.Pages
{
    public static class Constants
    {
        public const string Url = Base + Path;
        public const string Base = "file:///";
        public const string Path = "something";
    }

    // The following pages classes should all be valid with a Uri.AbsoluteUri equal to Constants.Url

    [PageObject(Constants.Url)]
    public class WithPathOnly : Page
    {
        public WithPathOnly() : base(null) { }
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


    // Page objects with base page objects.

    [PageObject(Constants.Path, Constants.Base)]
    public class WithPathAndBaseUrl : Page
    {
        public WithPathAndBaseUrl() : base(null) { }
    }

    // The following page classes should all be invalid when one is attempted to be created.

    [PageObject(null, typeof(string))]
    public class WithBaseThatIsNotAPage : Page
    {
        public WithBaseThatIsNotAPage() : base(null) { }
    }
}
