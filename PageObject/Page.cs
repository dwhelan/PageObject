using System.Collections.Generic;
using System.Linq;

namespace PageObject
{
    public abstract class Page
    {
        public string Host { get; private set; }
        public List<string> HostAliases { get; private set; }
        public string Path { get; private set; }
        public bool SSL { get; private set; }

        protected readonly PageSession Browser;

        protected Page(PageSession browser, string host, string path, bool ssl=false, params string[] hostAliases)
        {
            Host = host;
            HostAliases = hostAliases.ToList();
            HostAliases.Add(host);
            Path = path;
            SSL = ssl;

            if (browser != null)
            {
                Browser = browser;
                Browser.Configuration.AppHost = host;
                Browser.Configuration.SSL = SSL;
            }
        }

        public void Visit()
        {
            Browser.Visit(Path);
        }

        public string Title
        {
            get { return Browser.Title; }
        }
    }
}