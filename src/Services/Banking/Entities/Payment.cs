using System;

namespace Banking.Entities
{
    public class Payment
    {
        public PaymentStatus PaymentStatus { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime From { get; set; }
        public DateTime Due { get; set; }
    }
}