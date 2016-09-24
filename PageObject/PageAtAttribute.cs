using System;
using System.Collections.Generic;
using System.Linq;

namespace PageObject
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PageAtAttribute : Attribute
    {
        internal Uri Uri => UriBuilder.Build(BaseUri, Path);

        private Type BasePage { get; }
        private string BaseUrl { get; }
        private string Path { get; }

        public string[] SchemeMatch
        {
            get { return ShouldUseBasePageSchemeMatch ? For(BasePage).SchemeMatch : schemeMatch; }
            set { schemeMatch = value; }            
        }

        private bool ShouldUseBasePageSchemeMatch => schemeMatch.Length == 0 && BasePage != null;

        private string[] schemeMatch = new string[0];

        public int[] PortMatch
        {
            get { return ShouldUseBasePagePortMatch? For(BasePage).PortMatch : portMatch; }
            set { portMatch = value; }            
        }

        private bool ShouldUseBasePagePortMatch => portMatch.Length == 0 && BasePage != null;

        private int[] portMatch = new int[0];

        public string HostMatch
        {
            get { return ShouldUseBasePageHostMatch ? For(BasePage).HostMatch : hostMatch; }
            set { hostMatch = value; }
        }

        private bool ShouldUseBasePageHostMatch => hostMatch.Equals(MatchNothing) && BasePage != null;

        private string hostMatch = MatchNothing;

        public string PathMatch
        {
            get { return ShouldUseBasePagePathMatch ? For(BasePage).PathMatch : pathMatch; }
            set { pathMatch = value; }
        }

        public bool ShouldUseBasePagePathMatch => pathMatch.Equals(MatchNothing) && BasePage != null;

        private string pathMatch = MatchNothing;

        private static readonly string MatchNothing = "(?!.*)";

        public PageAtAttribute(Type basePage) : this(basePage, "")
        {
        }

        public PageAtAttribute(Type basePage, string path) : this(path)
        {
            BasePage = basePage;
        }

        public PageAtAttribute(string baseUrl, string path) : this(path)
        {
            BaseUrl = EnvironmentVariables.Expand(baseUrl);
        }

        public PageAtAttribute(string path)
        {
            Path = EnvironmentVariables.Expand(path);
        }

        internal Uri BaseUri
        {
            get
            {
                if (BasePage != null)
                    return For(BasePage).Uri;

                if (BaseUrl != null)
                    return UriBuilder.Build(BaseUrl);

                return null;
            }
        }

        internal void Validate(Type pageClass)
        {
            EnsureValidBasePage(pageClass);
            EnsureNoCircularReferencesInBasePages(pageClass);
            EnsureValidUri();
        }

        private void EnsureValidBasePage(Type pageClass)
        {
            if (BasePage == null || BasePage.IsSubclassOf(typeof(Page)))
                return;

            throw new PageObjectException(string.Format("The base page for {0} must be a subclass of {1}", pageClass, typeof(Page)));
        }

        private void EnsureNoCircularReferencesInBasePages(Type pageClass)
        {
            EnsureNoCircularReferencesInBasePages(pageClass, new List<Type>());
        }

        private void EnsureNoCircularReferencesInBasePages(Type pageClass, IList<Type> basePages)
        {
            if (basePages.Contains(pageClass))
            {
                basePages.Add(pageClass);
                throw new PageObjectException(string.Format("Detected circular base page references with {0}", string.Join(" => ", basePages.Select(p => p.FullName))));
            }

            var basePage = For(pageClass).BasePage;

            if (basePage != null)
            {
                basePages.Add(pageClass);
                EnsureNoCircularReferencesInBasePages(basePage, basePages);
            }
        }

        private void EnsureValidUri()
        {
            UriBuilder.Build(BaseUri, Path);
        }

        private static readonly IDictionary<Type, PageAtAttribute> PageObjectAttributes = new Dictionary<Type, PageAtAttribute>();

        internal static PageAtAttribute For(Type pageClass)
        {
            if (PageObjectAttributes.ContainsKey(pageClass))
                return PageObjectAttributes[pageClass];

            var attribute = GetCustomAttributes(pageClass).FirstOrDefault(a => a is PageAtAttribute);

            if (attribute == null)
                throw new PageObjectException(string.Format("Missing [PageAt(...))] attribute for {0}", pageClass));

            return PageObjectAttributes[pageClass] = (PageAtAttribute) attribute;
        }
    }
}
