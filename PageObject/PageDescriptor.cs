using System;
using System.Collections.Generic;

namespace PageObject
{
    internal class PageDescriptor
    {
        internal Uri Uri => UriBuilder.Build(Attribute.BaseUri, Attribute.Path);

        private PageAtAttribute Attribute { get; }

        internal PageDescriptor(Type pageClass)
        {
            Attribute = PageAtAttribute.For(pageClass);
        }

        private static readonly IDictionary<Type, PageDescriptor> PageDescriptors = new Dictionary<Type, PageDescriptor>();

        internal static PageDescriptor For(Type pageClass)
        {
            if (!PageDescriptors.ContainsKey(pageClass))
                PageDescriptors[pageClass] = new PageDescriptor(pageClass);

            return PageDescriptors[pageClass];
        }
    }
}
