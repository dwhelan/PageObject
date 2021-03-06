﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PageObject
{
    public class PageFactory
    {
        public static PageFactory Instance { get; } = new PageFactory();

        private readonly List<Type> pageClasses = new List<Type>();

        private PageFactory()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var pageClass in assembly.GetTypes().Where(IsValidPageClass))
                {
                    Add(pageClass);
                }
            }
        }

        internal void Add(Type pageClass)
        {
            pageClasses.Add(pageClass);
        }

        internal void Remove(Type pageClass)
        {
            pageClasses.Remove(pageClass);
        }

        public bool Contains(Type siteClass)
        {
            return pageClasses.Contains(siteClass);
        }

        public Page PageFor(string pageName, PageSession session = null)
        {
            return PageFor(PageClassFor(pageName), session);
        }

        public Page PageFor(Type pageClass, PageSession session = null)
        {
            const BindingFlags bindingFlags = BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance | BindingFlags.OptionalParamBinding;

            try
            {
                return (Page) Activator.CreateInstance(pageClass, bindingFlags, null, new object[] { session }, CultureInfo.CurrentCulture);
            }
            catch (TargetInvocationException x)
            {
                // ReSharper disable once PossibleNullReferenceException
                throw x.InnerException;
            }
        }

        public Type PageClassFor(string pageName)
        {
            var matches = pageClasses.Where(type => PageNameMatchesPageClass(pageName, type)).ToList();

            if (matches.Count == 0)
            {
                throw new ArgumentException($"Could not find page class for '{pageName}'.");
            }

            if (matches.Count > 1)
            {
                var matchingClassNames = string.Join(", ", matches.Select(pageClass => pageClass.FullName));
                throw new ArgumentException(string.Format("Found multiple page classes matching '{0}': {1}.", pageName, matchingClassNames));
            }

            return matches.First();
        }

        private static bool IsValidPageClass(Type type)
        {
            return type.IsSubclassOf(typeof(Page)) && !type.IsAbstract;
        }

        private static bool PageNameMatchesPageClass(string pageName, Type pageClass)
        {
            return RemovePunctuation(pageClass.FullName).EndsWith(RemovePunctuation(pageName));
        }

        private static string RemovePunctuation(string name1)
        {
            return Regex.Replace(name1, "\\W", "");
        }
    }
}
