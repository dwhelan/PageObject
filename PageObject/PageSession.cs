using System;
using Coypu;

namespace PageObject
{
    public class PageSession : IDisposable
    {
        public BrowserSession Browser { get; set; }
        public SessionConfiguration Configuration { get; }
        public Page Page { get; set; }

        public PageSession(SessionConfiguration sessionConfiguration)
        {
            Configuration = sessionConfiguration;
            Browser = new BrowserSession(sessionConfiguration);
        }

        private bool disposed = false;

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