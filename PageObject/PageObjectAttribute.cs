using System;

namespace PageObject
{
    public class PageObjectAttribute : Attribute
    {
        public Type BaseParent { get; }
        public string BaseUrl { get; }
        public string Path { get; }

        public PageObjectAttribute(string path)
        {
            Path = path;
        }

        public PageObjectAttribute(string path, Type baseParent)
        {
            BaseParent = baseParent;
            Path = path;
        }

        public PageObjectAttribute(string path, string baseUrl)
        {
            BaseUrl = baseUrl;
            Path = path;
        }
    }
}
