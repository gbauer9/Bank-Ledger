using System;

namespace BankLedger
{
    class Transaction
    {
        // Amount of transaction (always positive)
        public decimal amount { get; }

        // Time the transaction was made
        public DateTime time { get; }

        // Type of transaction (withdraw or deposit)
        public string type { get; }

        // Transaction class constructor
        public Transaction(decimal amount, DateTime time, string type)
        {
            this.amount = amount;
            this.time = time;
            this.type = type;
        }
    }
}