using PageObject;

namespace PageObjectTests.Pages
{
    public static class Constants
    {
        public const string Url = "file:///" + Path;
        public const string Path = "something";
    }

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

    [Page(typeof(string))]
    public class WithParentThatIsNotAPage : Page
    {
        public WithParentThatIsNotAPage() : base(null) { }
    }
}
