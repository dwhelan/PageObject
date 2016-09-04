using System;
using Coypu;

namespace PageObject
{
    public class PageSession : IDisposable
    {
        public BrowserSession Browser { get; set; }
        public SessionConfiguration Configuration { get; }
        public Page Page { get; set; }

        public PageSession(SessionConfiguration configuration)
        {
            Configuration = configuration;
            Browser = new BrowserSession(configuration);
        }

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Browser?.Dispose();
            }

            disposed = true;
        }
    }
}