using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Banking.Data.Entitites;

namespace Banking.Data.Repository
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetRange(int pageIndex, int pageSize);
        void Update(TEntity entity);
        
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        
    }
}