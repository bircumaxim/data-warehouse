using Banking.Data.Repository;

namespace Banking.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbApplicationContext _context;

        public UnitOfWork(DbApplicationContext context)
        {
            _context = context;
            ClientRepository = new ClientRepository(_context);
            PaymentRepository = new PaymentRepoistory(_context);
            TransactionRepository= new TransactionRepository(_context);
        }

        public IClientRepository ClientRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public ITransactionRepository TransactionRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}