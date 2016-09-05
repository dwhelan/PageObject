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

    [Page(typeof(WithPathOnly), Constants.Path)]
    public class WithParentAndPath : Page
    {
        public WithParentAndPath() : base(null) { }
    }

    [Page(typeof(WithPathOnly))]
    public class WithParentAndMissingPath : Page
    {
        public WithParentAndMissingPath() : base(null) { }
    }

    [Page(typeof(WithPathOnly), null)]
    public class WithParentAndNullPath : Page
    {
        public WithParentAndNullPath() : base(null) {}
    }

    [Page(typeof(WithPathOnly), "")]
    public class WithParentAndEmptyPath : Page
    {
        public WithParentAndEmptyPath() : base(null) {}
    }

    // The following page classes should all be invalid when one is attempted to be created.

    [Page(typeof(string))]
    public class WithParentThatIsNotAPage : Page
    {
        public WithParentThatIsNotAPage() : base(null) { }
    }
}
