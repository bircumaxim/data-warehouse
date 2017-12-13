using System;

namespace Banking.Data.Entitites
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionTime { get; set; }
        public int TaxRate { get; set; }

        public Transaction()
        {
            TransactionTime = DateTime.UtcNow;
        }
    }
}