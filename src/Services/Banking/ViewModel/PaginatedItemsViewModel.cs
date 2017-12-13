using System.Collections.Generic;
using Banking.Data.Entitites;

namespace Banking.ViewModel
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : IEntity
    {
        public int Offset { get; }

        public int Limit{ get; }

        public long Count { get; }

        public IEnumerable<TEntity> Data { get; }

        public PaginatedItemsViewModel(int offset, int limit, long count, IEnumerable<TEntity> data)
        {
            Offset = offset;
            Limit = limit;
            Count = count;
            Data = data;
        }
    }
}