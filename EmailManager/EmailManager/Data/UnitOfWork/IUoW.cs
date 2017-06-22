using EmailManager.Data.Repository;
using EmailManager.Models;
using System;
using System.Threading.Tasks;

namespace EmailManager.Data.UnitOfWork
{
    public interface IUoW : IDisposable
    {
        Lazy<IRepo<TEntity>> Get<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();

        bool EnsureCreated();
    }
}
