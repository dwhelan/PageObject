using PageObject;

namespace PageObjectTests.Pages
{
    public static class Constants
    {
        public const string Url = "file:///" + Path;
        public const string Path = "something";
    }

    // The following pages classes should all be valid with a Uri.AbsoluteUri equal to Constants.Url

    [Page(Constants.Url)]
    public class WithPathOnly : Page
    {
        public WithPathOnly() : base(null) { }
    }

    [Page(Constants.Path, typeof(WithPathOnly))]
    public class WithPathAndParent : Page
    {
        public WithPathAndParent() : base(null) { }
    }

    [Page(null, typeof(WithPathOnly))]
    public class WithNullPathAndParent : Page
    {
        public WithNullPathAndParent() : base(null) {}
    }

    [Page("", typeof(WithPathOnly))]
    public class WithEmptyPathAndParent : Page
    {
        public WithEmptyPathAndParent() : base(null) {}
    }

    // The following page classes should all be invalid when one is attempted to be created.

    [Page(null, typeof(string))]
    public class WithParentThatIsNotAPage : Page
    {
        public WithParentThatIsNotAPage() : base(null) { }
    }
}
