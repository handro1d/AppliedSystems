using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppliedSystems.Domain;

namespace AppliedSystems.Interfaces
{
    public interface IAppliedSystemsRepository<TEntity> where TEntity : IEntity
    {
        ICollection<TEntity> GetAll();

        TEntity Get(int id);

        TEntity Find(Expression<Func<TEntity, bool>> match);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);

        TEntity Add(TEntity t);

        Task<TEntity> AddAsync(TEntity t);

        TEntity Update(TEntity updatedEntity, int key);

        void Delete(TEntity t);
    }
}
