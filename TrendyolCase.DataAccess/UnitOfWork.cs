using System;
using System.Collections.Generic;
using System.Text;

namespace TrendyolCase.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private TrendyolContext context = new TrendyolContext();
        private GenericRepository<WebLinkDeepLink> webLinkDeepRepository = null;
        private GenericRepository<RegexPattern> regexPatternpRepository = null;
        public GenericRepository<WebLinkDeepLink> WebLinkDeepRepository
        {
            get
            {
                return this.webLinkDeepRepository ?? new GenericRepository<WebLinkDeepLink>(context);
            }
        }
        public GenericRepository<RegexPattern> RegexPatternpRepository
        {
            get
            {
                return this.regexPatternpRepository ?? new GenericRepository<RegexPattern>(context);
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
