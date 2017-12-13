using System;
using Banking.Data.Entitites;

namespace Banking.Entities
{
    public class Payment : IEntity
    {
        public Guid Id { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime From { get; set; }
        public DateTime Due { get; set; }
    }
}