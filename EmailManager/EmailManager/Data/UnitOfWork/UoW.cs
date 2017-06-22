using EmailManager.Data.Repository;
using EmailManager.Models;
using System;
using System.Threading.Tasks;

namespace EmailManager.Data.UnitOfWork
{
    public class UoW : IUoW
    {
        private readonly DatabaseContext _context;
        private bool disposed = false;

        public UoW(DatabaseContext context)
        {
            _context = context;

        }

        public Lazy<IRepo<TEntity>> Get<TEntity>() where TEntity : BaseEntity
        {
            return new Lazy<IRepo<TEntity>>(() => new Repo<TEntity>(_context));
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public bool EnsureCreated()
        {
            return _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
