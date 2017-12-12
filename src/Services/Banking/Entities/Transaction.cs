using System;

namespace Banking.Entities
{
    public class Transaction
    {
        public int Amount { get; set; }
        public DateTime TransactionTime { get; set; }
        public int TaxRate { get; set; }
    }
}