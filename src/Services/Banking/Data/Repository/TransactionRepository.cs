using Banking.Data.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Banking.Data.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        protected TransactionRepository(DbContext context) : base(context)
        {
        }
    }
}