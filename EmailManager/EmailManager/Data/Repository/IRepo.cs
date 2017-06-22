using EmailManager.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmailManager.Data.Repository
{
    public interface IRepo<TEntity> where TEntity : BaseEntity
    {
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<int> Count(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefault();

        TEntity ElementAt(int index);

        Task Update(TEntity entity);

        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        Task Delete(TEntity entity);

        Task DeleteRange(IEnumerable<TEntity> entities);
    }
}
