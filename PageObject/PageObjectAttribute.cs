using System;

namespace PageObject
{
    public class PageObjectAttribute : Attribute
    {
        public Type Parent { get; }
        public string Path { get; }

        public PageObjectAttribute(string path)
        {
            Path = path;
        }

        public PageObjectAttribute(string path, Type parent)
        {
            Parent = parent;
            Path = path;
        }
    }
}
