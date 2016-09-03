using System;
using System.Collections.Generic;
using System.Linq;

namespace PageObject
{
    public abstract class Page
    {
        public Uri Uri { get; private set; }
        public List<string> Hosts { get; private set; }

        protected readonly PageSession Browser;

        protected Page(PageSession browser, string host, string path, bool ssl=false, params string[] hostAliases)
        {
            Hosts = hostAliases.ToList();
            Hosts.Add(host);

            if (browser != null)
            {
                Browser = browser;
                Browser.Configuration.AppHost = host;
                Browser.Configuration.SSL = ssl;
            }
        }

        protected Page(PageSession browser, string url, params string[] hostAliases)
        {
            Uri = new Uri(url);
            if (Uri.Host.Length == 0)
            {
                Uri = new UriBuilder(Uri.Scheme, "localhost", Uri.Port, Uri.LocalPath).Uri;
            }
            Hosts = hostAliases.ToList();
            Hosts.Add(Uri.Host);

            if (browser != null)
            {
                Browser = browser;
                Browser.Configuration.AppHost = Uri.Host.Length == 0 ? "localhost" : Uri.Host;
                Browser.Configuration.SSL = Uri.Scheme == Uri.UriSchemeHttps;
            }
        }

        public void Visit()
        {
            Browser.Visit(Uri.AbsoluteUri);
        }

        public string Title
        {
            get { return Browser.Title; }
        }
    }
}