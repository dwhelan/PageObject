using System;
using System.IO;

namespace PageObject.SpecFlow.Tests.Pages.File
{
    internal static class Root
    {
        internal static string  Url => Uri.AbsoluteUri;
        internal static Uri     Uri => new UriBuilder(Uri.UriSchemeFile, "", 0, Path).Uri;
        private  static string Path => Directory.GetCurrentDirectory() + @"\..\..\Pages\File\";
    }
}
