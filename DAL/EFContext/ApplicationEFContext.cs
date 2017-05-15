using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public abstract class ApplicationEFContext<T> : IDisposable where T : class
    {
        public MallDBContext context;

        bool disposed = false;

        public ApplicationEFContext()
        {
            context = new MallDBContext();
        }

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

        public T FindById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }


        public ICollection<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<int> DeleteAsync(T t)
        {
            context.Set<T>().Remove(t);
            return await context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            T forDelete = await context.Set<T>().FindAsync(id);
            if (forDelete != null)
            {
                context.Set<T>().Remove(forDelete);
            }
        }

        public async Task<T> AddOrUpdateAsync(T updated, int id)
        {
            if (updated == null) return null;

            T res;
            T existing = await context.Set<T>().FindAsync(id);
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(updated);
                await context.SaveChangesAsync();
                res = existing;
            }
            else
            {
                context.Set<T>().Add(updated);
                await context.SaveChangesAsync();
                res = updated;
            }
            return res;
        }

    }
}
