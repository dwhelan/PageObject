using System;

namespace PageObject
{
    public class PageObjectAttribute : Attribute
    {
        public Type BasePage { get; }
        public string BaseUrl { get; }
        public string Path { get; }

        public PageObjectAttribute(string path, Type basePage) : this(path)
        {
            BasePage = basePage;
        }

        public PageObjectAttribute(string path, string baseUrl) : this(path)
        {
            BaseUrl = baseUrl;
        }

        public PageObjectAttribute(string path)
        {
            Path = path;
        }
    }
}
