using System;
using Banking.Data.Repository;

namespace Banking.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository ClientRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        int Complete();
    }
}