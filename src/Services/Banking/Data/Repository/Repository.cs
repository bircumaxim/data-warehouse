using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Banking.Data.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Banking.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetRange(int pageIndex, int pageSize)
        {
            return Context.Set<TEntity>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }

        public void Add(TEntity entitity)
        {
            Context.Set<TEntity>().Add(entitity);
        }

        public void Remove(TEntity entitity)
        {
            Context.Set<TEntity>().Add(entitity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}