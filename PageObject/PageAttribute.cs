using System;

namespace PageObject
{
    public class PageAttribute : Attribute
    {
        public Type Parent { get; }
        public string Path { get; }

        public PageAttribute(string path)
        {
            Path = path;
        }

        public PageAttribute(string path, Type parent)
        {
            Parent = parent;
            Path = path;
        }
    }
}
