using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Banking.Data.Entitites;

namespace Banking.Data.Repository
{
    public interface IRepository<TEntity> where TEntity: IEntity
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetRange(int pageIndex, int pageSize);
        
        void Add(TEntity entitity);
        void Remove(TEntity entitity);
        void AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        
    }
}