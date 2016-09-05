using System;
using NUnit.Framework.Constraints;

namespace PageObject
{
    public class PageAttribute : Attribute
    {
        public Type ParentPageClass { get; }
        public string Path { get; }

        public PageAttribute(string path)
        {
            Path = path;
        }

        public PageAttribute(Type parentPageClass, string path = null)
        {
            ParentPageClass = parentPageClass;
            Path = path;
        }
    }
}
