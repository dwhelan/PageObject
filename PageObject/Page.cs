using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using WatiN.Core.Comparers;

namespace PageObject
{
    public abstract class Page
    {
        public string Scheme { get; private set; }
        public int Port { get; private set; }
        public string Host { get; private set; }
        public List<string> HostAliases { get; private set; }
        public string Path { get; private set; }
        public bool SSL { get; private set; }
        public Uri Uri { get; private set; }

        protected readonly PageSession Browser;

        protected Page(PageSession browser, string host, string path, bool ssl=false, params string[] hostAliases)
        {
            Host = host;
            HostAliases = hostAliases.ToList();
            HostAliases.Add(host);
            Path = path;
            SSL = ssl;
            Port = 80;

            if (browser != null)
            {
                Browser = browser;
                Browser.Configuration.AppHost = host;
                Browser.Configuration.SSL = SSL;
            }
        }

        protected Page(PageSession browser, string url, params string[] hostAliases)
        {
            Uri = new Uri(url);
            Scheme = Uri.Scheme;
            SSL = Uri.Scheme == Uri.UriSchemeHttps;
            Port = Uri.Port;
            Host = Uri.Host.Length == 0 ? "localhost" : Uri.Host;
            HostAliases = hostAliases.ToList();
            HostAliases.Add(Host);
            Path = Uri.LocalPath;

            if (browser != null)
            {
                Browser = browser;
                Browser.Configuration.AppHost = Host;
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