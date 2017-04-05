using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class ApplicationEFContext : IDisposable
    {
        public MallDBContext context = new MallDBContext();

        bool disposed = false;

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
                context.Dispose();
            }
            disposed = true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
