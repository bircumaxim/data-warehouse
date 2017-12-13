using System;

namespace Banking.Data.Entitites
{
    public class Payment : IEntity
    {
        public Guid Id { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime From { get; set; }
        public DateTime Due { get; set; }

        public Payment()
        {
            From = DateTime.UtcNow;
        }
        
        public Payment(Transaction transaction, DateTime from, DateTime due)
        {
            Transaction = transaction;
            From = from;
            Due = due;
        }

    }
}